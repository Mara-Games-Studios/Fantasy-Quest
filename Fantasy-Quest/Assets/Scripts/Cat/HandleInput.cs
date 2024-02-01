namespace Cat
{
    public class HandleInput
    {
        public GameplayInput CatInput;

        public HandleInput(GameplayInput input)
        {
            CatInput = input;
        }

        private float InputValue => CatInput.Player.HorizontalMove.ReadValue<float>();

        public float GetHorizontalInput()
        {
            return InputValue;
        }

        public void EnableInput()
        {
            CatInput.Enable();
        }

        public void DisableInput()
        {
            CatInput.Disable();
        }
    }
}
