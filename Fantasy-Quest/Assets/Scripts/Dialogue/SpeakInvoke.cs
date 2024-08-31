using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.SpeakInvoke")]
    internal class SpeakInvoke : MonoBehaviour, ISpeakable
    {
        public UnityEvent OnSpeakTriggered;
        public UnityEvent OnSpeakStopped;

        public void Speak()
        {
            OnSpeakTriggered?.Invoke();
        }

        public void Stop()
        {
            OnSpeakStopped?.Invoke();
        }
    }
}
