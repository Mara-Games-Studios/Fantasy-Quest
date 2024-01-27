using UnityEngine;

public class MoveState : BaseState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly StateMashineData Data;
    protected readonly Cat CatPlayer;

    public MoveState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        this.CatPlayer = cat;
    }

    protected HandleInput Input => CatPlayer.Input;

    public override void Enter()
    {
        AddInputActionCallback();
    }

    public override void Exit()
    {
        RemoveInputActionCallback();
    }

    public override void Update()
    {
        Debug.Log(GetType());
    }

    protected virtual void AddInputActionCallback() { }

    protected virtual void RemoveInputActionCallback() { }

    protected bool IsHorizontalMoveZero() => Data.XInput == 0;

    public override void CheckInput()
    {
        Data.XInput = Input.GetHorizontalInput();
    }
}
