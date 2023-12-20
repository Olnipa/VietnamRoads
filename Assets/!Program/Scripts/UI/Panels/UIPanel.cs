using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _titleText;
    [SerializeField] protected MoneyModel _moneyModel;

    [SerializeField] private Button _closeButton;

    public event Action CloseButtonClicked;
    public event Action Opened;

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public virtual void Initialize()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public virtual void ShowPanel(Model modelToShow)
    {
        gameObject.SetActive(true);
        Opened.Invoke();
    }

    protected void OnCloseButtonClick()
    {
        CloseButtonClicked.Invoke();
    }
}