using System;
using Rails;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cat.Jump
{
    [Serializable]
    internal class JumpZone
    {
        [RequiredIn(PrefabKind.InstanceInScene)]
        [SerializeField]
        private RailsImpl targetRails;
        public RailsImpl TargetRails => targetRails;

        [MinMaxSlider(RailsImpl.MIN_TIME, RailsImpl.MAX_TIME)]
        [SerializeField]
        private Vector2 points = new(0.2f, 0.8f);

        public float StartPoint => points.x;
        public float EndPoint => points.y;

        public bool IsPointInZone(float time)
        {
            return time >= StartPoint && time <= EndPoint;
        }

        public Vector3 StartPointVec => targetRails.Path.GetPointAtTime(StartPoint);
        public Vector3 EndPointVec => targetRails.Path.GetPointAtTime(EndPoint);

        public void DrawGizmo()
        {
#if UNITY_EDITOR
            if (targetRails == null)
            {
                return;
            }
            Handles.color = Color.yellow;
            Handles.DrawLine(StartPointVec, EndPointVec, 7f);
#endif
        }
    }

    [AddComponentMenu("Scripts/Cat/Jump/Cat.Jump.PreparedJumpZone")]
    internal class PreparedJumpZone : MonoBehaviour
    {
        [SerializeField]
        private Cat.Vector moveVector;

        [SerializeField]
        private JumpDirection jumpDirection;

        [SerializeField]
        private JumpZone jumpFrom;

        [SerializeField]
        private JumpZone jumpTo;

        public Path.PrepareResult GetPrepreparedJumpConfig(RailsImpl fromRails)
        {
            if (jumpFrom.TargetRails != fromRails)
            {
                return new Path.PrepareResult() { Found = false };
            }

            if (!jumpFrom.IsPointInZone(fromRails.CurrentPosition))
            {
                return new Path.PrepareResult() { Found = false };
            }

            float time =
                (fromRails.CurrentPosition - jumpFrom.StartPoint)
                / (jumpFrom.EndPoint - jumpFrom.StartPoint);

            GroundMask mask = jumpTo.TargetRails.GetComponent<GroundMask>();

            Path.PrepareResult result =
                new()
                {
                    DestinationRails = jumpTo.TargetRails,
                    DestinationRailsTime = Mathf.Lerp(jumpTo.StartPoint, jumpTo.EndPoint, time),
                    Found = true,
                    MoveVector = moveVector,
                    JumpDirection = jumpDirection,
                    EndCatScale = mask.IsCatScaleChange ? mask.NewCatScale : null,
                };

            return result;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            jumpFrom.DrawGizmo();
            jumpTo.DrawGizmo();

            if (jumpFrom.TargetRails == null || jumpTo.TargetRails == null)
            {
                return;
            }

            Handles.DrawLine(jumpFrom.StartPointVec, jumpTo.StartPointVec, 7f);
            Handles.DrawLine(jumpFrom.EndPointVec, jumpTo.EndPointVec, 7f);
#endif
        }
    }
}
