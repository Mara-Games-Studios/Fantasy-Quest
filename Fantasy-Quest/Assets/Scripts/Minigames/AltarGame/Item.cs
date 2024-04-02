using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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

        [SerializeField]
        private List<SpriteRenderer> spriteRenderers;

        public void SetNewLayerOrder(int order)
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.sortingOrder = order;
            }
        }

        [Button]
        private void GenerateUid()
        {
            uid = Guid.NewGuid().ToString();
        }

        public bool Compare(Item other)
        {
            return other.Uid == uid;
        }
    }
}
