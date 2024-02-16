using Cutscene;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interaction.Item
{
    //After first cutscene played, script switches icon hint and on new interactable button plays new cutscene
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.CutsceneInteractTransition")]
    internal class CutsceneInteractTransition : MonoBehaviour, IInteractable
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
