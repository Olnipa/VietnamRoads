using System;

public class UIPanelsSwitcher : IDisposable
{
    private ClickedCityDetector _clickedCityDetector;
    private UIStateMachine _uiStateMachine;
    private CityPanel _cityPanel;
    private ProvinceUnlockPanel _provinceUnlockPanel;
    private CameraMoverSwitcher _cameraMoverSwitcher;

    public UIPanelsSwitcher(ClickedCityDetector clickedCityDetector, UIStateMachine uiStateMachine, CityPanel cityPanel, ProvinceUnlockPanel provincePanel, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _clickedCityDetector = clickedCityDetector;
        _uiStateMachine = uiStateMachine;
        _cityPanel = cityPanel;
        _provinceUnlockPanel = provincePanel;
        _cameraMoverSwitcher = cameraMoverSwitcher;

        _provinceUnlockPanel.OkButtonClicked += OnProvinceUnlockButtonClick;
        _clickedCityDetector.CityChosed += OnCityChoose;
        _cityPanel.CloseButtonClicked += OnCloseUIButtonClicked;
    }

    public void Dispose()
    {
        _provinceUnlockPanel.OkButtonClicked -= OnProvinceUnlockButtonClick;
        _clickedCityDetector.CityChosed -= OnCityChoose;
        _cityPanel.CloseButtonClicked -= OnCloseUIButtonClicked;
    }

    private void OnCityChoose()
    {
        _uiStateMachine.ChangeState(new UICityPanelState(_cityPanel, _clickedCityDetector.ClickedCity, _cameraMoverSwitcher));
    }

    private void OnCloseUIButtonClicked()
    {
        _uiStateMachine.ExitCurrentState();
    }

    public void OnProvinceUnlockButtonClick(ProvinceView provinceView)
    {
        _uiStateMachine.ExitCurrentState();
    }

    public void OnLockProvinceClick(ProvinceView provinceView)
    {
        _uiStateMachine.ChangeState(new UIProvinceUnlockPanelState(_provinceUnlockPanel, provinceView, _cameraMoverSwitcher));
    }
}