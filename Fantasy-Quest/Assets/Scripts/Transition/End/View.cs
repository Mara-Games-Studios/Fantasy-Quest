using UnityEngine;
using UnityEngine.Events;

namespace Transition.End
{
    [AddComponentMenu("Scripts/Scripts/Transitions/End/Transitions.End.View")]
    internal class View : MonoBehaviour
    {
        public UnityEvent AppearanceEndCallback;

        // Called by animation clip event
        public void AppearanceEnd()
        {
            AppearanceEndCallback?.Invoke();
        }
    }
}
