using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Pages
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/UI/Pages/Buttons/UI.Pages.Buttons.IndicatedButton")]
    public class IndicatedButton : Button
    {
        public static Action<IndicatedButton> OnPointerEntered;
        public static Action<IndicatedButton> OnPointerExited;
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnPointerEntered?.Invoke(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnPointerExited?.Invoke(this);
        }
    }
}
