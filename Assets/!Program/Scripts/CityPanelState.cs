public class CityPanelState : IUIState
{
    private CityPanel _cityPanel;

    public CityPanelState(CityPanel cityPanel)
    {
        _cityPanel = cityPanel;
    }

    public void Enter()
    {
        _cityPanel.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _cityPanel.gameObject.SetActive(false);
    }
}
