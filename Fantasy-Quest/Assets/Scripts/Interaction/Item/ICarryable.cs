namespace Interaction.Item
{
    internal interface ICarryable
    {
        public bool CanCatCarry();
        public void CarryByCat();
        public void CarryByHuman();
    }
}
