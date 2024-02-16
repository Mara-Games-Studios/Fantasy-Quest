using Cutscene;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.HumanCutsceneStarter")]
    internal class HumanCutsceneStarter : MonoBehaviour, IInteractable
    {
        [Required]
        [SerializeField]
        private Start startCat;

        [Required]
        [SerializeField]
        private Start startHuman;

        [Required]
        [SerializeField]
        private DialogueBubble.Trigger trigger;

        [Required]
        [SerializeField]
        private Sprite newIcon;

        private bool canCatInteract;
        bool IInteractable.CanCatInteract
        {
            get => canCatInteract;
            set => canCatInteract = value;
        }

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                startCat.StartCutscene();
                Debug.Log("Drink Milk");
                Destroy(this);
            }
        }

        public void InteractionByHuman()
        {
            if (!canCatInteract)
            {
                startHuman.StartCutscene();
                if (trigger != null)
                {
                    ChangeShortcutIcon();
                }

                canCatInteract = true;
            }
        }

        private void ChangeShortcutIcon()
        {
            trigger.SetNewIcon(newIcon);
        }
    }
}
