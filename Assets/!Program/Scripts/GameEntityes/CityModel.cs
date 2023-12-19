using System;
using System.Collections.Generic;
using UnityEngine;

public class CityModel : Model
{
    private ProvinceModel _provinceModel;

    private int _defaultCarsCount = 3;
    private int _additionalCarsOnLevelUp = 3;
    private int _maxMotorbikeLevel = 3;
    private int _maxCarLevel = 3;
    private int _maxBusLevel = 3;

    private float _minTimeBetweenCar = 4.5f;
    private float _maxTimeBetweenCar = 9f;
    private float _timeToWaitAvailableVehicle = 3f;
    private int _defaultVehiclesCount = 30;

    public string Name { get; private set; }
    public HashSet<CityModel> NearestConnectedCities { get; private set; } = new HashSet<CityModel>();
    public Vector2 Position { get; private set; }
    public ProvinceModel ProvinceModel => _provinceModel;
    public int CurrentBikeLevel { get; private set; } = 1;
    public int CurrentCarLevel { get; private set; } = 1;
    public int CurrentBusLevel { get; private set; } = 1;

    public event Action<CityModel> CityConnected;

    public CityModel(Vector2 position, string cityName)
    {
        Position = position;
        Name = cityName;
    }

    public void AddConnectedCity(CityModel connectedCityModel)
    {
        NearestConnectedCities.Add(connectedCityModel);
        CityConnected?.Invoke(connectedCityModel);
    }
}