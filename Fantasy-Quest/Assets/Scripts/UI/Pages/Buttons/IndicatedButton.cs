using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Pages
{
    [RequireComponent(typeof(Button))]
    public class IndicatedButton: Button
    {
        public static Action<IndicatedButton> OnPointerEntered;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnPointerEntered?.Invoke(this);
        }
    }
}
