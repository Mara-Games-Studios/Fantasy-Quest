using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteAlways]
    [AddComponentMenu("Scripts/UI/UI.SizeRepeater")]
    internal class SizeRepeater : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private RectTransform reference;

        [SerializeField]
        private Vector2 addiction;

        [ReadOnly]
        [SerializeField]
        private RectTransform rectTransform;

        private void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(reference);
            if (reference.sizeDelta.magnitude >= 0.001)
            {
                rectTransform.sizeDelta = reference.sizeDelta + addiction;
            }
            else
            {
                rectTransform.sizeDelta = Vector2.zero;
            }
        }
    }
}
