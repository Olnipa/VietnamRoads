using TMPro;
using UnityEngine;

public class CityPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cityName;
    [SerializeField] private int _motorbikeLevel;
    [SerializeField] private int _carLevel;
    [SerializeField] private int _busLevel;

    public void ShowPanel(City city)
    {
        _cityName.text = city.name;

    }

}