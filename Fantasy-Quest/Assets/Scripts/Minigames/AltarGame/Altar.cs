using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Altar")]
    internal class Altar : MonoBehaviour
    {
        [SerializeField]
        private List<Slot> slots;

        public IEnumerable<Slot> GetFreeSlots()
        {
            return slots.Where(x => !x.IsItemPlaced);
        }

        public bool IsAllRightPlaced()
        {
            return slots.All(x => x.IsPlacedCorrectItem());
        }

        [Button]
        public void CatchAllSlots()
        {
            slots = GetComponentsInChildren<Slot>().ToList();
        }
    }
}
