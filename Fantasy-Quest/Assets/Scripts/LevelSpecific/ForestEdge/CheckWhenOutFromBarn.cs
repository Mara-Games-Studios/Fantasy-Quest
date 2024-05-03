using Configs.Progression;
using UnityEngine;
using Utils;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.CheckWhenOutFromBarn"
    )]
    internal class CheckWhenOutFromBarn : MonoBehaviour
    {
        [SerializeField]
        private bool triggered = false;

        [SerializeField]
        private TransformMover moveCat;

        public void Check()
        {
            if (ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed && !triggered)
            {
                triggered = true;
                moveCat.Move();
            }
        }
    }
}
