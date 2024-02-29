using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Item")]
    internal class Item : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private string uid;
        public string Uid => uid;

        [Button]
        private void GenerateUid()
        {
            uid = GUID.Generate().ToString();
        }

        public bool Compare(Item other)
        {
            return other.Uid == uid;
        }
    }
}
