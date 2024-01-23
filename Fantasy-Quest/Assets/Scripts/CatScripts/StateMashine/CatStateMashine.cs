using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatStateMashine :IStateSwitcher
{
    private List<BaseState> states;
    private StateMashineData data;
    private BaseState currentState;

    public StateMashineData Data => data;

    public CatStateMashine(Cat cat)
    {
         data = new StateMashineData();
        
        states = new List<BaseState>()
        {
            new IdleState(this, data, cat),
            new RunState(this, data, cat),
            new UpJumpState(this, data, cat),
            new DownJumpState(this, data, cat),
            new FallState(this, data, cat)
        };

        currentState = states[0];
        currentState.Enter();
    }

    public void SwitchState<State>() where State : BaseState
    {
        BaseState state = states.FirstOrDefault(s => s is State);

        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Update() => currentState.Update();

    public void CheckInput() => currentState.CheckInput();
}
