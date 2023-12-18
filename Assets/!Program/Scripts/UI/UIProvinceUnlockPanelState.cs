public class UIProvinceUnlockPanelState : IUIState
{
    private ProvinceUnlockPanel _provinceUnlockPanel;
    private ProvinceView _provinceView;
    private CameraMoverSwitcher _cameraMoverSwitcher;

    public UIProvinceUnlockPanelState(ProvinceUnlockPanel provinceUnlockPanel, ProvinceView clickedProvinceView, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _provinceView = clickedProvinceView;
        _provinceUnlockPanel = provinceUnlockPanel;
        _cameraMoverSwitcher = cameraMoverSwitcher;
    }

    public void Enter()
    {
        _cameraMoverSwitcher.Disable();
        _provinceUnlockPanel.ShowProvincePanel(_provinceView);
    }

    public void Exit()
    {
        _cameraMoverSwitcher.Enable();
        _provinceUnlockPanel.gameObject.SetActive(false);
    }
}