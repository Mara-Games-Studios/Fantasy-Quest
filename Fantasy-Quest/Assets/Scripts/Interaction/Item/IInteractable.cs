namespace Interaction.Item
{
    internal interface IInteractable
    {
        public bool CanCatInteract { get; }

        public void InteractionByCat();
        public void InteractionByHuman();
    }
}
