using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.EditorControlPanel")]
    internal class EditorControlPanel : MonoBehaviour
    {
        [SerializeField]
        private Paw paw;

        public void SetPawSpeed(string value)
        {
            paw.Speed = float.Parse(value);
        }
    }
}
