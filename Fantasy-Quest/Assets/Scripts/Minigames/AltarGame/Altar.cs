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
        private Manager manager;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private List<Slot> slots;

        public void TurnOnAltar()
        {
            animator.enabled = true;
        }

        // Must be called by animation clip
        public void AltarTurnedOnEnd()
        {
            manager.TellWinAndQuit();
        }

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
