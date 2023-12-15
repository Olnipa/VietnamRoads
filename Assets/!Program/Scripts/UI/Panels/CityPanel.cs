using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cityName;
    [SerializeField] private int _motorbikeLevel;
    [SerializeField] private int _carLevel;
    [SerializeField] private int _busLevel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _universalCloseButton;

    public event Action CloseButtonClicked;

    public void Initialize()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _universalCloseButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        _universalCloseButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public void ShowPanel(City city)
    {
        gameObject.SetActive(true);
        _cityName.text = city.name;
        _universalCloseButton.gameObject.SetActive(true);
    }

    private void OnCloseButtonClick()
    {
        CloseButtonClicked.Invoke();
        gameObject.SetActive(false);
        _universalCloseButton.gameObject.SetActive(false);
    }
}