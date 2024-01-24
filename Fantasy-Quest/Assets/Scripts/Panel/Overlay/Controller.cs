using UnityEngine;
using UnityEngine.Events;

namespace Panel.Overlay
{
    [AddComponentMenu("Scripts/Panel/Overlay/Panel.Overlay.Controller")]
    internal class Controller : MonoBehaviour
    {
        public UnityEvent PauseActionPerformed;

        // Must be called by UI (button)
        public void PauseActionCallback()
        {
            PauseActionPerformed?.Invoke();
        }
    }
}
