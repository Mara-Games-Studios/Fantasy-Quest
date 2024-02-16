using Cat;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Flipper")]
    internal class Flipper : MonoBehaviour
    {
        [SerializeField]
        private Movement catMovement;

        private void OnEnable()
        {
            catMovement.OnVectorChanged += VectorChanged;
        }

        private void OnDisable()
        {
            catMovement.OnVectorChanged -= VectorChanged;
        }

        [Button]
        private void VectorChanged(Vector vector)
        {
            // TODO: implement
        }
    }
}
