using System;
using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.SquirrelBody")]
    internal class SquirrelBody : MonoBehaviour
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
