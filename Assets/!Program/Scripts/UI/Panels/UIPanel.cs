using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _titleText;
    [SerializeField] protected MoneyModel _moneyModel;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _universalCloseButton;

    public event Action CloseButtonClicked;

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        _universalCloseButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public virtual void Initialize()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _universalCloseButton.onClick.AddListener(OnCloseButtonClick);
    }

    public virtual void ShowPanel(Model titleText)
    {
        gameObject.SetActive(true);
        _titleText.text = titleText;
        _universalCloseButton.gameObject.SetActive(true);
    }

    protected void OnCloseButtonClick()
    {
        CloseButtonClicked?.Invoke();
        _universalCloseButton.gameObject.SetActive(false);
    }
}