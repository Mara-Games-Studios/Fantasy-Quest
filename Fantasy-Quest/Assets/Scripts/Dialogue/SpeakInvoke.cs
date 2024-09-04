using Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.SpeakInvoke")]
    internal class SpeakInvoke : MonoBehaviour, IInteractable
    {
        public UnityEvent OnSpeakTriggered;

        public void Interact()
        {
            Speak();
        }

        public void Speak()
        {
            OnSpeakTriggered?.Invoke();
        }
    }
}
