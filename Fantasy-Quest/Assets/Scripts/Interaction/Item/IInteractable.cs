namespace Interaction.Item
{
    internal interface IInteractable
    {
        public bool CanCatInteract();
        public void InteractionByCat();
        public void InteractionByHuman();
    }
}
