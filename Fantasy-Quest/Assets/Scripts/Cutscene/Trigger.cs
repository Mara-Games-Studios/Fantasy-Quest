using Interaction;
using UnityEngine;

namespace Cutscene
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Trigger")]
    internal class Trigger : MonoBehaviour
    {
        [SerializeField]
        private Start starter;

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<InteractionImpl>(out _))
            {
                starter.StartCutscene();
                Destroy(gameObject);
            }
        }
    }
}
