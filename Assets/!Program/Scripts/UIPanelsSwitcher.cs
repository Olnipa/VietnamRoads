public class UIPanelsSwitcher
{
    private CityDetector _cityDetector;
    private UIStateMachine _uiStateMachine;
    private CityPanel _cityPanel;

    public UIPanelsSwitcher(CityDetector cityDetector, UIStateMachine uiStateMachine, CityPanel cityPanel)
    {
        _cityDetector = cityDetector;
        _uiStateMachine = uiStateMachine;
        _cityPanel = cityPanel;

        _cityDetector.CityChosed += OnCityChoose;
    }

    private void OnCityChoose()
    {
        _uiStateMachine.ChangeState(new CityPanelState(_cityPanel));
    }
}