﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _titleText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _universalCloseButton;

    public event Action CloseButtonClicked;

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        _universalCloseButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public void Initialize()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _universalCloseButton.onClick.AddListener(OnCloseButtonClick);
    }

    public virtual void ShowPanel(string titleText)
    {
        gameObject.SetActive(true);
        _titleText.text = titleText;
        _universalCloseButton.gameObject.SetActive(true);
    }

    protected void OnCloseButtonClick()
    {
        CloseButtonClicked.Invoke();
        gameObject.SetActive(false);
        _universalCloseButton.gameObject.SetActive(false);
    }
}