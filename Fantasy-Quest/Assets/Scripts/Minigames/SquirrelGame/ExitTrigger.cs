using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.ExitTrigger")]
    internal class ExitTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Paw paw))
            {
                paw.ExitReached();
            }
        }
    }
}
