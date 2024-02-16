using Cutscene;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interaction.Item
{
    //Cutscene played depends on canCatInteract condition(Can = E, Can't = 1)
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.CutsceneInteractable")]
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
                Destroy(this);
            }
        }

        public void InteractionByHuman()
        {
            if (!canCatInteract)
            {
                cutsceneStarter.StartCutscene();
                Destroy(this);
            }
        }
    }
}
