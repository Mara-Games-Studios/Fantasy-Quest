using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.SquirrelBody")]
    internal class SquirrelBody : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Paw paw))
            {
                paw.SquirrelTouch();
            }
        }
    }
}
