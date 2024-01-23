public interface IStateSwitcher 
{
    void SwitchState<State>() where State : BaseState;
}
