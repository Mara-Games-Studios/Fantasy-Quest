using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

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

    [Serializable]
    public struct IndicatorsButtons
    {
        public Button Left;
        public Button Right;
    }
}
