﻿public class UICityPanelState : IUIState
{
    private CityPanel _cityPanel;
    private CityModel _clickedCityModel;
    private CameraMoverSwitcher _cameraMoverSwitcher;

    public UICityPanelState(CityPanel cityPanel, CityModel clickedCityModel, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _clickedCityModel = clickedCityModel;
        _cityPanel = cityPanel;
        _cameraMoverSwitcher = cameraMoverSwitcher;
    }

    public void Enter()
    {
        _cameraMoverSwitcher.Disable();
        _cityPanel.ShowPanel(_clickedCityModel);
    }

    public void Exit()
    {
        _cameraMoverSwitcher.Enable();
        _cityPanel.gameObject.SetActive(false);
    }
}