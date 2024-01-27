using Cat.StateMachine.States;

namespace Cat.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<State>()
            where State : StateBase;
    }
}
