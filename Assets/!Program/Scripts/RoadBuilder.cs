using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    [SerializeField] private int _roadsPoolCount = 50;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private LinePositionSetter _linePositionSetter;
    [SerializeField] private Road _roadPrefab;

    private ClickedCityDetector _cityDetector;
    private MainParameterModel _moneyModel;

    private List<Road> _roads = new List<Road>();

    public event Action<CityView, CityView> RoadAdded;

    public void Initialize(ClickedCityDetector cityDetector, MainParameterModel moneyModel)
    {
        _cityDetector = cityDetector;
        _moneyModel = moneyModel;

        for (int i = 0; i < _roadsPoolCount; i++)
        {
            _roads.Add(InstantiateNewRoad());
        }

        _cityDetector.CityConnected += OnCityConnected;
    }

    private void OnDisable()
    {
        _cityDetector.CityConnected -= OnCityConnected;
    }

    private void OnCityConnected()
    {
        TryCreateRoad(_linePositionSetter.StartLinePosition, _linePositionSetter.EndLinePosition, _cityDetector.ClickedCity.CityModel, _cityDetector.ConnectedCity.CityModel);
    }

    public void TryCreateRoad(Vector2 startPosition, Vector2 endPosition, CityModel firstConnectedCity, CityModel secondConnectedCity)
    {
        if (firstConnectedCity.NearestConnectedCities.Contains(secondConnectedCity))
            return;

        float roadLengh = Mathf.Abs((endPosition - startPosition).magnitude);
        int roadTotalPrice = Mathf.RoundToInt(roadLengh * PriceList.RoadUnitPrice);

        if (_moneyModel.TryRemoveValue(roadTotalPrice) == false)
            return;
        
        Road road = _roads.FirstOrDefault(road => road.gameObject.activeSelf == false);

        if (road == null)
            road = InstantiateNewRoad();

        road.Initialize(startPosition, endPosition, firstConnectedCity, secondConnectedCity);
    }

    private Road InstantiateNewRoad()
    {
        Road road = Instantiate(_roadPrefab, transform);
        road.gameObject.SetActive(false);
        return road;
    }
}