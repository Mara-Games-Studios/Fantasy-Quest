using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    
    [Serializable]
    public struct PageInfo
    {
        [Required]
        public CanvasGroup CanvasGroup;

        [Required]
        public RectTransform RectTransform;
    }

    [Serializable]
    public struct PivotPoints
    {
        public RectTransform StartPoint;
        public RectTransform MiddlePoint;
        public RectTransform EndPoint;
    }
}
