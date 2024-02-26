using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.SquirrelBody")]
    internal class SquirrelBody : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Pat pat))
            {
                pat.SquirrelTouch();
            }
        }
    }
}
