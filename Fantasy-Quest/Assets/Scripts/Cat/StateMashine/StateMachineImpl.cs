using System.Collections.Generic;
using System.Linq;
using Cat.StateMachine.States;
using Cat.StateMachine.States.Air;
using Cat.StateMachine.States.Ground;

namespace Cat.StateMachine
{
    public class StateMachineImpl : IStateSwitcher
    {
        private List<StateBase> states;
        private StateMachineData data;
        private StateBase currentState;

        public StateMachineData Data => data;

        public StateMachineImpl(CatImpl cat)
        {
            data = new StateMachineData();

            states = new List<StateBase>()
            {
                new Idle(this, data, cat),
                new Run(this, data, cat),
                new UpJump(this, data, cat),
                new DownJump(this, data, cat),
                new Falling(this, data, cat)
            };

            currentState = states[0];
            currentState.Enter();
        }

        public void SwitchState<State>()
            where State : StateBase
        {
            StateBase state = states.FirstOrDefault(s => s is State);
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }

        public void Update()
        {
            currentState.Update();
        }

        public void CheckInput()
        {
            currentState.CheckInput();
        }
    }
}
