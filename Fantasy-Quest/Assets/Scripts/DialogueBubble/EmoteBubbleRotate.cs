using Cat;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.EmoteBubbleRotate")]
    internal class EmoteBubbleRotate : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Movement catMovement;

        private void OnEnable()
        {
            catMovement.OnVectorChanged += StateChanged;
        }

        private void OnDisable()
        {
            catMovement.OnVectorChanged -= StateChanged;
        }

        private void StateChanged(Vector direction)
        {
            switch (direction)
            {
                case Vector.Left:
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                case Vector.Right:
                    transform.rotation = Quaternion.Euler(0f, 0, 0f);
                    break;
            }
            ;
        }

        [Button]
        public void FindCat()
        {
            catMovement = FindAnyObjectByType<Movement>().GetComponent<Movement>();
        }
    }
}
