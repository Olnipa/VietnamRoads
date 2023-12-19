public class UIProvinceUnlockPanelState : IUIState
{
    private ProvinceUnlockPanel _provinceUnlockPanel;
    private ProvinceModel _provinceModel;
    private CameraMoverSwitcher _cameraMoverSwitcher;

    public UIProvinceUnlockPanelState(ProvinceUnlockPanel provinceUnlockPanel, ProvinceModel clickedProvinceModel, CameraMoverSwitcher cameraMoverSwitcher)
    {
        _provinceModel = clickedProvinceModel;
        _provinceUnlockPanel = provinceUnlockPanel;
        _cameraMoverSwitcher = cameraMoverSwitcher;
    }

    public void Enter()
    {
        _cameraMoverSwitcher.Disable();
        _provinceUnlockPanel.ShowPanel(_provinceModel);
    }

    public void Exit()
    {
        _cameraMoverSwitcher.Enable();
        _provinceUnlockPanel.gameObject.SetActive(false);
    }
}