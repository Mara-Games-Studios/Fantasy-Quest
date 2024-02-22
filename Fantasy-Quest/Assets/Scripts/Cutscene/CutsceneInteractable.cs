using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    //Cutscene played depends on canCatInteract condition(Can = E, Can't = 1)
    [AddComponentMenu("Scripts/Cutscene/Cutscene.CutsceneInteract")]
    internal class CutsceneInteractable : MonoBehaviour, IInteractable
    {
        [Required]
        [SerializeField]
        private Start cutsceneStarter;

        [SerializeField]
        private bool canCatInteract;
        bool IInteractable.CanCatInteract => canCatInteract;

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                cutsceneStarter.StartCutscene();
                gameObject.SetActive(false);
            }
        }

        public void InteractionByHuman()
        {
            if (!canCatInteract)
            {
                cutsceneStarter.StartCutscene();
                gameObject.SetActive(false);
            }
        }
    }
}
