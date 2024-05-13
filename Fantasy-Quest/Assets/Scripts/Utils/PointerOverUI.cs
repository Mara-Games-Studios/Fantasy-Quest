using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Utils
{
    public class PointerOverUI
    {
        private static readonly int uiLayer = LayerMask.NameToLayer("UI");

        public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaycastResults[index];
                if (curRaysastResult.gameObject.layer == uiLayer)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData =
                new(EventSystem.current) { position = Mouse.current.position.ReadValue() };
            List<RaycastResult> raysastResults = new();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
    }
}
