public class UIStateMachine
{
    private IUIState _currentState;

    public void ChangeState<T>(T newState) where T : IUIState
    {
        ExitCurrentState();
        _currentState = newState;
        _currentState.Enter();
    }

    public void ExitCurrentState()
    {
        _currentState?.Exit();
    }
}
