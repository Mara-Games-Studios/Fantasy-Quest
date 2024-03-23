using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Pages
{
    [Serializable]
    public class Model
    {
        public PageInfo PageInfo;

        public RectTransform StartPoint;

        public RectTransform EndPoint;

        public bool IsUsingMovement = true;

        public bool IsActiveOnAwake = false;

        public float MinAlpha = 0.3f;

        public float MaxAlpha = 1f;

        public float Duration = 1f;
    }

    [Serializable]
    public struct PageInfo
    {
        [Required]
        public CanvasGroup CanvasGroup;

        [Required]
        public RectTransform RectTransform;
    }
}
