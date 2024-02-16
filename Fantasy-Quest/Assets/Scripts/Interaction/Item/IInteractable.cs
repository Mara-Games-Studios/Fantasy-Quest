namespace Interaction.Item
{
    internal interface IInteractable
    {
        public bool CanCatInteract
        {
            set => CanCatInteract = value;
            get => CanCatInteract;
        }

        public void InteractionByCat();
        public void InteractionByHuman();
    }
}
