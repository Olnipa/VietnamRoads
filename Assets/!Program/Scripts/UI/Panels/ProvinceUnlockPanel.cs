using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProvinceUnlockPanel : UIPanel
{
    [SerializeField] private Button _okButton;
    [SerializeField] private TextMeshProUGUI _priceToUnlockProvince;
    [SerializeField] private string _titleProvinceToOpen;

    private ProvinceModel _provinceModel;

    public event Action<ProvinceModel> OkButtonClicked;

    public override void ShowPanel(Model modelToShow)
    {
        if (modelToShow is ProvinceModel provinceModel)
        {
            base.ShowPanel(modelToShow);
            _provinceModel = provinceModel;
            _titleText.text = _titleProvinceToOpen;
            _priceToUnlockProvince.text = PriceList.UnlockNextProvincePrice.ToString();
            _okButton.onClick.AddListener(OnOkButtonClick);
        }
    }

    private void OnOkButtonClick()
    {
        OkButtonClicked.Invoke(_provinceModel);
    }

    private void OnDisable()
    {
        _okButton.onClick.RemoveListener(OnOkButtonClick);
    }
}