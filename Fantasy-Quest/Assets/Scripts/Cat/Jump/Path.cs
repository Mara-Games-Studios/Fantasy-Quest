using System;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using Rails;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat.Jump
{
    public enum Quad
    {
        RightUp,
        RightDown,
        LeftUp,
        LeftDown,
    }

    public enum JumpDirection
    {
        Up,
        Down,
    }

    [Serializable]
    public struct PointedVector
    {
        public Vector2 Time;
        public Vector2 Value;
    }

    [Serializable]
    public class PointedCurve
    {
        [SerializeField]
        private List<PointedVector> values;

        public Vector2 GetCurveValue(Vector2 point)
        {
            Vector2 result = Vector2.zero;

            values = values.OrderBy(x => x.Time.x).ToList();
            result.x = Evaluate(
                values.Select(v => v.Time.x).ToList(),
                values.Select(v => v.Value.x).ToList(),
                values.Count,
                point.x
            );
            result.y = Evaluate(
                values.Select(v => v.Time.y).ToList(),
                values.Select(v => v.Value.y).ToList(),
                values.Count,
                point.y
            );
            return result;
        }

        private float Evaluate(List<float> times, List<float> values, int count, float time)
        {
            for (int i = 0; i < count - 1; i++)
            {
                float l_time = times[i];
                float r_time = times[i + 1];
                if (l_time <= time && time <= r_time)
                {
                    float l_val = values[i];
                    float r_val = values[i + 1];
                    float norm_time = (time - l_time) / (r_time - l_time);
                    return Mathf.Lerp(l_val, r_val, norm_time);
                }
            }

            Debug.LogError(
                $"Don't found needed value. Requested value {time}, bounds: {times.First()} -> {times.Last()}"
            );
            return 0;
        }
    }

    [AddComponentMenu("Scripts/Cat/Jump/Cat.Jump.Path")]
    internal class Path : MonoBehaviour
    {
        [SerializeField]
        private RailsImpl rails;
        public RailsImpl Rails => rails;
        private BezierPath BezierPath => Rails.BezierPath;
        private PathCreator PathCreator => rails.PathCreator;

        [SerializeField]
        private Vector2 maxJumpPoint;

        [SerializeField]
        private PointedCurve startPointCurve;

        [SerializeField]
        private PointedCurve endPointCurve;

        [SerializeField]
        private Vector3 leftRotation;

        [SerializeField]
        private Vector3 rightRotation;

        [SerializeField]
        private Vector2 catPositionShift;

        [SerializeField]
        private int calculationSteps;

        [SerializeField]
        private float thresholdDistance;

        [SerializeField]
        private Vector3 stashPosition;

        [SerializeField, ReadOnly]
        private List<GroundMask> groundMasks;
        private IEnumerable<RailsImpl> AvailableRails =>
            groundMasks.Where(x => x.IsAvailable).Select(x => x.Rails);

        private void Start()
        {
            groundMasks = FindObjectsByType<GroundMask>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.None
                )
                .ToList();
            PathCreator.InitializeEditorData(true);
        }

        public void StashPath()
        {
            transform.position = stashPosition;
            // Set end point
            BezierPath.MovePoint(0, Vector3.left);
            BezierPath.MovePoint(3, Vector3.right);

            // Set curve points
            BezierPath.MovePoint(1, Vector3.one);
            BezierPath.MovePoint(2, -Vector3.one);
        }

        public struct PrepareResult
        {
            public bool Found;
            public RailsImpl DestinationRails;
            public float DestinationRailsTime;
        }

        public PrepareResult PreparePath(
            JumpDirection jumpDirection,
            Vector2 catPosition,
            Cat.Vector moveVector
        )
        {
            Vector2 absoluteCatPos = catPosition;
            catPosition += catPositionShift;

            // Perfect jump point
            Quad quad = GetQuad(jumpDirection, moveVector);
            Vector2 maxPoint = catPosition + (maxJumpPoint * QuadToVector(quad));
            Vector2 downMaxPoint = new(maxPoint.x, catPosition.y);
            float heightDelta = maxPoint.y - catPosition.y;
            if (jumpDirection == JumpDirection.Up)
            {
                downMaxPoint.y -= heightDelta;
            }
            RailsImpl targetRail = GetNearestRailToSegment(AvailableRails, maxPoint, downMaxPoint);
            Vector3 targetPoint = targetRail.GetClosestPointOnPath(maxPoint);
            Vector2 jumpPoint = targetPoint - (Vector3)catPosition;

            if (jumpDirection == JumpDirection.Down && absoluteCatPos.y <= targetPoint.y)
            {
                return new PrepareResult { Found = false };
            }

            // Prepare path
            transform.position = catPosition;

            // Set end point
            BezierPath.MovePoint(0, Vector3.zero);
            BezierPath.MovePoint(3, jumpPoint);

            // Set curve points
            Vector2 normalized = jumpPoint / maxJumpPoint;
            normalized.x = Mathf.Clamp01(Mathf.Abs(normalized.x));
            normalized.y = Mathf.Clamp(normalized.y, -1, 1);
            Vector2 start = startPointCurve.GetCurveValue(normalized);
            Vector2 end = endPointCurve.GetCurveValue(normalized);

            if (moveVector is Cat.Vector.Left)
            {
                start.x *= -1;
                end.x *= -1;
            }

            BezierPath.MovePoint(1, start);
            BezierPath.MovePoint(2, (Vector2)BezierPath.GetPoint(3) + end);

            BezierPath.NotifyPathModified();

            return new PrepareResult()
            {
                Found = true,
                DestinationRails = targetRail,
                DestinationRailsTime = targetRail.Path.GetClosestTimeOnPath(maxPoint)
            };
        }

        private RailsImpl GetNearestRailToSegment(
            IEnumerable<RailsImpl> availableRails,
            Vector2 start,
            Vector2 end
        )
        {
            Vector2 step = (end - start) / calculationSteps;
            Vector2 point = start;
            RailsImpl result = null;
            float shortestDistance = float.MaxValue;

            for (int i = 0; i < calculationSteps; i++)
            {
                foreach (RailsImpl rail in availableRails)
                {
                    float distance = Vector3.Distance(
                        point,
                        rail.Path.GetClosestPointOnPath(point)
                    );
                    if (distance <= thresholdDistance)
                    {
                        return rail;
                    }
                    if (shortestDistance >= distance)
                    {
                        result = rail;
                        shortestDistance = distance;
                    }
                }

                point += step;
            }

            return result;
        }

        private Quad GetQuad(JumpDirection jumpDirection, Cat.Vector moveVector)
        {
            return (jumpDirection, moveVector) switch
            {
                (JumpDirection.Up, Cat.Vector.Right) => Quad.RightUp,
                (JumpDirection.Up, Cat.Vector.Left) => Quad.LeftUp,
                (JumpDirection.Down, Cat.Vector.Right) => Quad.RightDown,
                (JumpDirection.Down, Cat.Vector.Left) => Quad.LeftDown,
                _ => throw new System.Exception()
            };
        }

        // Suppose that all rails is strongly horizontal
        private List<RailsImpl> FilterRails(Quad targetQuad, Vector2 anchor)
        {
            return AvailableRails
                .Where(x =>
                    targetQuad == DetectQuad(anchor, x.Path.GetPointAtTime(0))
                    || targetQuad == DetectQuad(anchor, x.Path.GetPointAtTime(0.999f))
                )
                .ToList();
        }

        private Quad DetectQuad(Vector2 anchor, Vector2 point)
        {
            return (anchor.x <= point.x, anchor.y <= point.y) switch
            {
                (true, true) => Quad.RightUp,
                (true, false) => Quad.RightDown,
                (false, true) => Quad.LeftUp,
                (false, false) => Quad.LeftDown,
            };
        }

        private Vector2 QuadToVector(Quad quad)
        {
            return quad switch
            {
                Quad.LeftDown => Vector2.left + Vector2.down,
                Quad.LeftUp => Vector2.left + Vector2.up,
                Quad.RightUp => Vector2.right + Vector2.up,
                Quad.RightDown => Vector2.right + Vector2.down,
                _ => throw new System.Exception()
            };
        }
    }
}
