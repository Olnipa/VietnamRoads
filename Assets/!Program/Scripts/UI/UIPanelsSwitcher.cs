using System;

public class UIPanelsSwitcher : IDisposable
{
    private ClickedCityDetector _clickedCityDetector;
    private UIStateMachine _uiStateMachine;
    private CityPanel _cityPanel;
    private ProvinceUnlockPanel _provinceUnlockPanel;
    private CameraMoverSwitcher _cameraMoverSwitcher;
    private UniversalCloseButtonSwitcher _universalCloseButton;

    public bool PanelIsOpen { get; private set; }

    public UIPanelsSwitcher(ClickedCityDetector clickedCityDetector, UIStateMachine uiStateMachine, CityPanel cityPanel, ProvinceUnlockPanel provincePanel, UniversalCloseButtonSwitcher universalCloseButton, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _clickedCityDetector = clickedCityDetector;
        _uiStateMachine = uiStateMachine;
        _cityPanel = cityPanel;
        _provinceUnlockPanel = provincePanel;
        _universalCloseButton = universalCloseButton;
        _cameraMoverSwitcher = cameraMoverSwitcher;

        _uiStateMachine.UIPanelOpened += OnUIOpen;
        _uiStateMachine.UIPanelClosed += OnUIClose;
        _provinceUnlockPanel.OkButtonClicked += OnProvinceUnlockButtonClick;
        _clickedCityDetector.CityChosed += OnCityChoose;
        _cityPanel.CloseButtonClicked += OnCloseUIButtonClicked;
        _provinceUnlockPanel.CloseButtonClicked += OnCloseUIButtonClicked;
        _universalCloseButton.Clicked += OnCloseUIButtonClicked;
    }

    public void OnUIOpen() => PanelIsOpen = true;

    public void OnUIClose() => PanelIsOpen = false;

    public void OnProvinceUnlockButtonClick(ProvinceModel provinceModel)
    {
        OnCloseUIButtonClicked();
    }

    public void OnLockProvinceClick(ProvinceModel provinceModel)
    {
        if (PanelIsOpen)
            return;

        _uiStateMachine.ChangeState(new UIProvinceUnlockPanelState(_provinceUnlockPanel, provinceModel, _cameraMoverSwitcher));
    }

    public void Dispose()
    {
        _uiStateMachine.UIPanelOpened -= OnUIOpen;
        _uiStateMachine.UIPanelClosed -= OnUIClose;
        _provinceUnlockPanel.OkButtonClicked -= OnProvinceUnlockButtonClick;
        _clickedCityDetector.CityChosed -= OnCityChoose;
        _cityPanel.CloseButtonClicked -= OnCloseUIButtonClicked;
        _provinceUnlockPanel.CloseButtonClicked -= OnCloseUIButtonClicked;
        _universalCloseButton.Clicked -= OnCloseUIButtonClicked;
    }

    private void OnCityChoose(CityModel cityModel)
    {
        if (PanelIsOpen)
            return;

        _uiStateMachine.ChangeState(new UICityPanelState(_cityPanel, cityModel, _cameraMoverSwitcher));
    }

    private void OnCloseUIButtonClicked()
    {
        _uiStateMachine.ExitCurrentState();
    }
}