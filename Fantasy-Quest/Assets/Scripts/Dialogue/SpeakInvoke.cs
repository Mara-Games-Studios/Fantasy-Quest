using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.SpeakInvoke")]
    internal class SpeakInvoke : MonoBehaviour, ISpeakable
    {
        [InfoBox("CALLED BY 1")]
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
