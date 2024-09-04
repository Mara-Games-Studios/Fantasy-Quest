namespace Interaction
{
    internal interface IInteractable
    {
        virtual int GetPriority()
        {
            return 0;
        }

        void Interact();
    }
}
