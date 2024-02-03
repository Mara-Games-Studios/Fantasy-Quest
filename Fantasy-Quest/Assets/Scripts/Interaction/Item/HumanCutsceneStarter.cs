using Cutscene;
using UnityEngine;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.Bowl")]
    internal class HumanCutsceneStarter : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Start start;
        public bool CanCatInteract => true;

        public void InteractionByCat()
        {
            return;
        }

        public void InteractionByHuman()
        {
            start.StartCutscene();
            Destroy(this);
        }
    }
}
