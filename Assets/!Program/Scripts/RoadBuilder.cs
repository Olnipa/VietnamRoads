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

    private CityDetector _cityDetector;

    private List<Road> _roads = new List<Road>();

    public event Action<City, City> RoadAdded;

    public void Initialize(CityDetector cityDetector)
    {
        _cityDetector = cityDetector;

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
        TryCreateRoad(_linePositionSetter.StartLinePosition, _linePositionSetter.EndLinePosition, _cityDetector.ClickedCity, _cityDetector.ConnectedCity);
    }

    public void TryCreateRoad(Vector2 startPosition, Vector2 endPosition, City firstConnectedCity, City secondConnectedCity)
    {
        if (firstConnectedCity.NearestConnectedCities.Contains(secondConnectedCity))
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