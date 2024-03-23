using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Pages
{
    [Serializable]
    public class Model
    {
        public PageInfo pageInfo;

        public RectTransform startPoint;

        public RectTransform endPoint;

        public bool isUsingMovement = true;

        public bool isActiveOnAwake = false;

        public float minAlpha = 0.3f;

        public float maxAlpha = 1f;

        public float duration = 1f;
    }

    [Serializable]
    public struct PageInfo
    {
        [Required]
        public CanvasGroup canvasGroup;

        [Required]
        public RectTransform rectTransform;
    }
}
