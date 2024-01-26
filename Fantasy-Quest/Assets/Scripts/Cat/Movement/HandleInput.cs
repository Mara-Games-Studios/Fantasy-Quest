using UnityEngine;

public  class HandleInput 
{
    public CatInput CatInput;
    private float moveValue;

    public HandleInput(CatInput input)
    {
        CatInput = input;
    }

    private float InputValue => CatInput.Movement.HorizontalMove.ReadValue<float>();

    public float GetHorizontalInput()
    {
        return  InputValue;
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
