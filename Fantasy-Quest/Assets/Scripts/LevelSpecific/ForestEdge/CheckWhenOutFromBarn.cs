using Cat;
using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

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
        private MovementInvoke pavetnikPoint;

        [SerializeField]
        private MovementInvoke standartPoint;

        public UnityEvent AfterRails;

        public void Check()
        {
            if (ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed && !triggered)
            {
                triggered = true;
                pavetnikPoint.InvokeSetOnRails();
                AfterRails?.Invoke();
            }
            else
            {
                standartPoint.InvokeSetOnRails();
            }
        }
    }
}
