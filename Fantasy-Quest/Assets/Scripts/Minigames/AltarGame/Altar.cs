using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Altar")]
    internal class Altar : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager manager;

        [Required]
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private string normalAnimation;

        [SerializeField]
        private string riseAnimation;

        [SerializeField]
        private List<Slot> slots = new();

        public void TurnOnAltar()
        {
            animator.Play(riseAnimation);
        }

        public void ResetAltar()
        {
            animator.Play(normalAnimation);
            slots.ForEach(x => x.FreeSlot());
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
