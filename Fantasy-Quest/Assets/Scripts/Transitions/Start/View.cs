using UnityEngine;
using UnityEngine.Events;

namespace Transitions.Start
{
    [AddComponentMenu("Scripts/Scripts/Transitions/Start/Transitions.Start.View")]
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
