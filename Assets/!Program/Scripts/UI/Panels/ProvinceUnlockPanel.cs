using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProvinceUnlockPanel : UIPanel
{
    [SerializeField] private Button _okButton;
    [SerializeField] private TextMeshProUGUI _priceToUnlockProvince;
    [SerializeField] private string _titleProvinceToOpen;

    private ProvinceView _provinceView;

    public event Action<ProvinceView> OkButtonClicked;

    public override void ShowPanel(Model titleText)
    {
        base.ShowPanel(titleText);
    }

    public void ShowProvincePanel(ProvinceView provinceView)
    {
        base.ShowPanel(_titleProvinceToOpen + " " + provinceView.name + "?");
        _provinceView = provinceView;
        _priceToUnlockProvince.text = PriceList.UnlockNextProvincePrice.ToString();

        _okButton.onClick.AddListener(OnOkButtonClick);
    }

    private void OnOkButtonClick()
    {
        OkButtonClicked.Invoke(_provinceView);
        _okButton.onClick.RemoveListener(OnOkButtonClick);
    }
}