using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu(
        "Scripts/Minigames/SquirrelGame/Squirrel/Minigames.SquirrelGame.Squirrel.Body"
    )]
    internal class Body : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out ISquirrelTouchable touchable))
            {
                touchable.Touch();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISquirrelTouchable touchable))
            {
                touchable.UnTouch();
            }
        }
    }
}
