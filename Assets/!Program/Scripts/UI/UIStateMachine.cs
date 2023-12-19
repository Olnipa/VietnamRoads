using System;
using System.Diagnostics.Tracing;

public class UIStateMachine
{
    private IUIState _currentState;

    public event Action UIPanelOpened;
    public event Action UIPanelClosed;

    public void ChangeState<T>(T newState) where T : IUIState
    {
        ExitCurrentState();
        _currentState = newState;
        _currentState.Enter();
        UIPanelOpened.Invoke();
    }

    public void ExitCurrentState()
    {
        _currentState?.Exit();
        UIPanelClosed.Invoke();
    }
}