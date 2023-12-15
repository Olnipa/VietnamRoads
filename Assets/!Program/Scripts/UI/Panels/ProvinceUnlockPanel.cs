using System;
using UnityEngine;
using UnityEngine.UI;

public class ProvinceUnlockPanel : UIPanel
{
    [SerializeField] private Button _okButton;

    public event Action okButtonClicked;

    public override void ShowPanel(string titleText)
    {
        base.ShowPanel(titleText);
        _okButton.onClick.AddListener(OnOkButtonClick);
    }

    private void OnOkButtonClick()
    {
        _okButton.onClick.RemoveListener(OnOkButtonClick);
        okButtonClicked.Invoke();
    }
}