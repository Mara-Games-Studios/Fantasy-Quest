using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    //After first cutscene played, script switches icon hint and on new interactable button plays new cutscene
    [AddComponentMenu("Scripts/Cutscene/Cutscene.CutsceneInteractTransition")]
    internal class CutsceneInteractTransition : MonoBehaviour, IInteractable
    {
        [InfoBox("CALLED BY E AND CALLED BY 1")]
        [Required]
        [SerializeField]
        private Start startCat;

        [Required]
        [SerializeField]
        private Start startHuman;

        [Required]
        [SerializeField]
        private GameObject hint;

        [Required]
        [SerializeField]
        private Sprite newIcon;

        private bool canCatInteract;

        public void InteractByCat()
        {
            if (canCatInteract)
            {
                startCat.StartCutscene();
                Destroy(this);
            }
        }

        public void InteractByHuman()
        {
            if (!canCatInteract)
            {
                startHuman.StartCutscene();
                if (hint != null)
                {
                    ChangeShortcutIcon();
                }

                canCatInteract = true;
            }
        }

        private void ChangeShortcutIcon()
        {
            hint.GetComponent<SpriteRenderer>().sprite = newIcon;
        }
    }
}
