using DG.Tweening;
using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.DoTweenCleanerObject")]
    internal class DoTweenCleanerObject : MonoBehaviour
    {
        private void OnDestroy()
        {
            int count = DOTween.KillAll();
            Debug.Log($"Do Tween Cleaner kill {count} Tweens.");
        }
    }
}
