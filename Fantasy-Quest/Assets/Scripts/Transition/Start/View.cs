using UnityEngine;
using UnityEngine.Events;

namespace Transition.Start
{
    [AddComponentMenu("Scripts/Transitions/Start/Transitions.Start.View")]
    internal class View : MonoBehaviour
    {
        public UnityEvent FadeEndCallback;

        // Called by animation clip event
        public void FadeEnd()
        {
            FadeEndCallback?.Invoke();
        }
    }
}
