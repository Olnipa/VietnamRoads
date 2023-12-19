﻿public class UICityPanelState : IUIState
{
    private CityPanel _cityPanel;
    private CityView _city;
    private CameraMoverSwitcher _cameraMoverSwitcher;

    public UICityPanelState(CityPanel cityPanel, CityView clickedCity, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _city = clickedCity;
        _cityPanel = cityPanel;
        _cameraMoverSwitcher = cameraMoverSwitcher;
    }

    public void Enter()
    {
        _cameraMoverSwitcher.Disable();
        _cityPanel.ShowPanel(_city.name);
    }

    public void Exit()
    {
        _cameraMoverSwitcher.Enable();
        _cityPanel.gameObject.SetActive(false);
    }
}