using System;
using UnityEngine.UI;

public class UniversalCloseButtonSwitcher : IDisposable
{
    private UniversalCloseButton _universalCloseButton;
    private CityPanel _cityPanel;
    private ProvinceUnlockPanel _provincePanel;

    public event Action Clicked;

    public UniversalCloseButtonSwitcher(UniversalCloseButton universalCloseButton, CityPanel cityPanel, ProvinceUnlockPanel provincePanel)
    {
        _universalCloseButton = universalCloseButton;
        _cityPanel = cityPanel;
        _provincePanel = provincePanel;

        _universalCloseButton.onClick.AddListener(OnUniversalCloseButtonClick);

        _cityPanel.Opened += ShowUniversalCloseButton;
        _provincePanel.Opened += ShowUniversalCloseButton;

        _cityPanel.CloseButtonClicked += HideUniversalCloseButton;
        _provincePanel.CloseButtonClicked += HideUniversalCloseButton;
    }

    private void ShowUniversalCloseButton()
    {
        _universalCloseButton.gameObject.SetActive(true);
    }

    private void HideUniversalCloseButton()
    {
        _universalCloseButton.gameObject.SetActive(false);
    }

    private void OnUniversalCloseButtonClick()
    {
        Clicked.Invoke();
        HideUniversalCloseButton();
    }

    public void Dispose()
    {
        _universalCloseButton.onClick.RemoveListener(OnUniversalCloseButtonClick);

        _cityPanel.Opened -= ShowUniversalCloseButton;
        _provincePanel.Opened -= ShowUniversalCloseButton;

        _cityPanel.CloseButtonClicked -= HideUniversalCloseButton;
        _provincePanel.CloseButtonClicked -= HideUniversalCloseButton;
    }
}