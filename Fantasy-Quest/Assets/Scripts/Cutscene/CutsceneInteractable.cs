using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    //Cutscene played depends on canCatInteract condition(Can = E, Can't = 1)
    [AddComponentMenu("Scripts/Cutscene/Cutscene.CutsceneInteract")]
    internal class CutsceneInteractable : MonoBehaviour, IInteractable
    {
        [InfoBox("CALLED BY E AND CALLED BY 1")]
        [Required]
        [SerializeField]
        private Start cutsceneStarter;

        [SerializeField]
        private bool canCatInteract;

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                cutsceneStarter.StartCutscene();
                gameObject.SetActive(false);
            }
        }

        public void InteractByHuman()
        {
            if (!canCatInteract)
            {
                cutsceneStarter.StartCutscene();
                gameObject.SetActive(false);
            }
        }
    }
}
