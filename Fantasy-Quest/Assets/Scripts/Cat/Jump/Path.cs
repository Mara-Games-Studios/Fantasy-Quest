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
        public float Height;
        public Vector2 Position;
    }

    [Serializable]
    public class PointedCurve
    {
        [SerializeField]
        private List<PointedVector> values;

        public Vector2 GetCurveValue(float height)
        {
            return values[0].Position;
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
        private List<RailsImpl> groundRails;

        private void Start()
        {
            groundRails = FindObjectsByType<RailsImpl>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.None
                )
                .Where(x => x.TryGetComponent<GroundMask>(out _))
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
            Vector2 orCatPos = catPosition;
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
            RailsImpl targetRail = GetNearestToSegment(groundRails, maxPoint, downMaxPoint);
            Vector3 targetPoint = targetRail.Path.GetClosestPointOnPath(maxPoint);

            if (jumpDirection == JumpDirection.Down && orCatPos.y <= targetPoint.y)
            {
                return new PrepareResult { Found = false };
            }

            // Prepare path
            transform.position = catPosition;

            // Set end point
            BezierPath.MovePoint(0, Vector3.zero);
            BezierPath.MovePoint(3, targetPoint - transform.position);

            // Set curve points
            Vector2 start = startPointCurve.GetCurveValue(heightDelta);
            Vector2 end = endPointCurve.GetCurveValue(heightDelta);

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

        private RailsImpl GetNearestToSegment(
            List<RailsImpl> availableRails,
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
            return groundRails
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
