using System;

public class UIPanelsSwitcher : IDisposable
{
    private CityDetector _cityDetector;
    private UIStateMachine _uiStateMachine;
    private CityPanel _cityPanel;
    private CameraMoverSwitcher _cameraMoverSwitcher;

    public UIPanelsSwitcher(CityDetector cityDetector, UIStateMachine uiStateMachine, CityPanel cityPanel, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _cityDetector = cityDetector;
        _uiStateMachine = uiStateMachine;
        _cityPanel = cityPanel;
        _cameraMoverSwitcher = cameraMoverSwitcher;

        _cityDetector.CityChosed += OnCityChoose;
        _cityPanel.CloseButtonClicked += OnCloseUIButtonClicked;
    }

    public void Dispose()
    {
        _cityDetector.CityChosed -= OnCityChoose;
        _cityPanel.CloseButtonClicked -= OnCloseUIButtonClicked;
    }

    private void OnCityChoose()
    {
        _uiStateMachine.ChangeState(new UICityPanelState(_cityPanel, _cityDetector.ClickedCity, _cameraMoverSwitcher));
    }

    private void OnCloseUIButtonClicked()
    {
        _uiStateMachine.ExitCurrentState();
    }
}
