namespace Interaction.InteractableItem
{
    internal interface ICarryableItem
    {
        public bool CanCatCarry();
        public void CarryByCat();
        public void CarryByHuman();
    }
}
