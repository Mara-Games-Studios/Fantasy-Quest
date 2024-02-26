using Sirenix.OdinInspector;
using UnityEngine;

namespace Transition.End
{
    [AddComponentMenu("Scripts/Transitions/End/Transitions.End.ProgressBar")]
    internal class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private RectTransform mask;

        [ReadOnly]
        [SerializeField]
        private float maxWidth;

        private void Awake()
        {
            maxWidth = mask.sizeDelta.x;
            mask.sizeDelta = new Vector2(0, mask.sizeDelta.y);
        }

        public void SetProgress(float progress)
        {
            mask.sizeDelta = new Vector2(Mathf.Clamp01(progress) * maxWidth, mask.sizeDelta.y);
        }
    }
}
