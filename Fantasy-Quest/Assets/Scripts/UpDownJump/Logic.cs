using System;
using Cat;
using Rails;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace UpDownJump
{
    [Serializable]
    internal struct JumpSection
    {
        public float Duration;
        public Point RailPoint;
        public float MixDuration;
        public AnimationCurve MoveCurve;
        public AnimationReferenceAsset Animation;
    }

    [AddComponentMenu("Scripts/UpDownJump/UpDownJump.Logic")]
    internal class Logic : MonoBehaviour, IUpDownJump
    {
        [Required]
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private JumpSection jumpSection;

        [SerializeField]
        private JumpPath jumpPath;

        [SerializeField]
        private JumpPath debugUpJumpPath;

        [SerializeField]
        private JumpPath debugDownJumpPath;

        private bool isJumping;

        private void Update()
        {
            PrepareToJump();
            ShowDebugJumpPaths();
        }

        private void ShowDebugJumpPaths()
        {
            debugUpJumpPath.PreparePath(
                JumpDirection.Up,
                catMovement.transform.position,
                catMovement.Vector
            );
            debugDownJumpPath.PreparePath(
                JumpDirection.Down,
                catMovement.transform.position,
                catMovement.Vector
            );
        }

        private void PrepareToJump()
        {
            if (isJumping)
            {
                return;
            }
        }

        public void JumpDown()
        {
            if (isJumping)
            {
                return;
            }
        }

        public void JumpUp()
        {
            if (isJumping)
            {
                return;
            }
        }
    }
}
