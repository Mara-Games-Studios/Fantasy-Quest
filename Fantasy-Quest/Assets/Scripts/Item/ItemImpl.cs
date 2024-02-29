using Sirenix.OdinInspector;
using UnityEngine;

namespace Item
{
    [AddComponentMenu("Scripts/Item/Item")]
    internal partial class ItemImpl : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private string uid;
        public string Uid => uid;

        [SerializeField]
        private string itemName;
        public string ItemName => itemName;
    }
}
