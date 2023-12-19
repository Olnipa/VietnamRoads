using System;
using System.Diagnostics.Tracing;

public class UIStateMachine
{
    private IUIState _currentState;

    public event Action UIPanelOpened;
    public event Action UIPanelClosed;

    public void ChangeState<T>(T newState) where T : IUIState
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
        UIPanelOpened.Invoke();
    }

    public void ExitCurrentState()
    {
        _currentState?.Exit();
        _currentState = null;
        UIPanelClosed.Invoke();
    }
}