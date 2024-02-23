using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    //After first cutscene played, script switches icon hint and on new interactable button plays new cutscene
    [AddComponentMenu("Scripts/Cutscene/Cutscene.CutsceneInteractTransition")]
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

        bool IInteractable.CanCatInteract => throw new System.NotImplementedException();

        public void InteractByCat()
        {
            if (canCatInteract)
            {
                startCat.StartCutscene();
                gameObject.SetActive(false);
            }
        }

        public void InteractByHuman()
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
