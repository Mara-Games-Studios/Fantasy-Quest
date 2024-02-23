namespace Interaction.Item
{
    internal interface IInteractable
    {
        public bool CanCatInteract { get; }

        public void InteractByCat();
        public void InteractByHuman();
    }
}
