using TMPro;
using UnityEngine;

public class CityPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI _motorbikeLevel;
    [SerializeField] private TextMeshProUGUI _carLevel;
    [SerializeField] private TextMeshProUGUI _busLevel;

    public override void ShowPanel(Model modelToShow)
    {
        if (modelToShow is CityModel cityModel)
        {
            base.ShowPanel(modelToShow);
            _titleText.text = cityModel.Name;
            _motorbikeLevel.text = cityModel.CurrentBikeLevel.ToString();
            _carLevel.text = cityModel.CurrentCarLevel.ToString();
            _busLevel.text = cityModel.CurrentBusLevel.ToString();
        }
    }
}