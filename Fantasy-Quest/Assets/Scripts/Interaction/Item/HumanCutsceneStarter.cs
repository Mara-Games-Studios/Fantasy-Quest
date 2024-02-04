using Cutscene;
using UnityEngine;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.Bowl")]
    internal class HumanCutsceneStarter : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Start start;

        [SerializeField]
        private DialogueBubble.Trigger trigger;

        [SerializeField]
        private Sprite newIcon;
        public bool CanCatInteract => true;

        public void InteractionByCat()
        {
            return;
        }

        public void InteractionByHuman()
        {
            start.StartCutscene();
            if (trigger != null)
            {
                ChangeShortcutIcon();
            }

            Destroy(this);
        }

        private void ChangeShortcutIcon()
        {
            trigger.SetNewIcon(newIcon);
        }
    }
}
