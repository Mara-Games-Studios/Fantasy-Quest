using DG.Tweening;
using Effects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.RunningMouse")]
    internal class RunningMouse : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Fade fade;

        [SerializeField]
        private float runTime;

        [SerializeField]
        private Transform endPoint;

        private bool oneTime = false;

        public UnityEvent RunEnded;
        public UnityEvent RunStarted;

        [Button]
        public void Run()
        {
            if (oneTime)
            {
                return;
            }
            RunStarted?.Invoke();
            oneTime = true;
            _ = transform.DOMove(endPoint.position, runTime).OnComplete(() => RunEnded?.Invoke());
            _ = DOVirtual.DelayedCall(runTime - fade.Duration, () => fade.Disappear(), false);
        }
    }
}
