namespace Cat.StateMachine.States
{
    public abstract class StateBase
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly StateMachineData Data;
        protected readonly CatImpl Cat;

        public StateBase(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
            Cat = cat;
        }

        protected HandleInput Input => Cat.Input;

        public virtual void Enter()
        {
            AddInputActionCallback();
        }

        public virtual void Exit()
        {
            RemoveInputActionCallback();
        }

        public virtual void Update() { }

        protected virtual void AddInputActionCallback() { }

        protected virtual void RemoveInputActionCallback() { }

        protected bool IsHorizontalMoveZero()
        {
            return Data.XInput == 0;
        }

        public virtual void CheckInput()
        {
            Data.XInput = Input.GetHorizontalInput();
        }
    }
}
