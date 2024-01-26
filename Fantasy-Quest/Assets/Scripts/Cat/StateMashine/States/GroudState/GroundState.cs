using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundState : MoveState
{
    private GroundChecker groundChecker;

    public GroundState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {
        groundChecker = cat.GroundChecker;
    }

    public override void Update()
    {
        base.Update();

        if (groundChecker.IsTouch ==false)
        {
            StateSwitcher.SwitchState<FallState>();
        }
    }

    protected override void AddInputActionCallback()
    {
        base.AddInputActionCallback();
        Input.CatInput.Movement.UpJump.started += OnUpJumpKeyPressed;
        Input.CatInput.Movement.DownJump.started += OnDownJumpKeyPressed;
    }

    protected override void RemoveInputActionCallback()
    {
        base.RemoveInputActionCallback();
        Input.CatInput.Movement.UpJump.started -= OnUpJumpKeyPressed;
        Input.CatInput.Movement.DownJump.started -= OnDownJumpKeyPressed;
    }

    private void OnDownJumpKeyPressed(InputAction.CallbackContext context)
    {
        CatPlayer.SetActiveDownJumpType();
        StateSwitcher.SwitchState<DownJumpState>();
    }

    private void OnUpJumpKeyPressed(InputAction.CallbackContext context)
    {
        CatPlayer.SetActiveUpJumpType();
        StateSwitcher.SwitchState<UpJumpState>();
    }
}
