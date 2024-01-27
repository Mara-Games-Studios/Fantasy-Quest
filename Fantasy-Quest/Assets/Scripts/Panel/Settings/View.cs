using UnityEngine;
using UnityEngine.Events;

namespace Panel.Settings
{
    [AddComponentMenu("Scripts/Panel/Settings/Panel.Settings.View")]
    internal class View : MonoBehaviour
    {
        public UnityEvent OnSettingsClosed;

        // Must be called by animation clip event
        public void HideAnimationEndCallback()
        {
            OnSettingsClosed?.Invoke();
        }
    }
}
