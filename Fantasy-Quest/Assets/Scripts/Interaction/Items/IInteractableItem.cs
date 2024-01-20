namespace Interaction.InteractableItem
{
    internal interface IInteractableItem
    {
        public bool CanCatInteract();
        public void InteractionByCat();
        public void InteractionByHuman();
    }
}
