using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CityView : View
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ProvinceView _provinceView;

    [SerializeField] public readonly int _defaultCarsCount = 3;
    [SerializeField] private int _additionalCarsOnLevelUp = 3;
    [SerializeField] private int _maxMotorbikeLevel = 3;
    [SerializeField] private int _maxCarLevel = 3;
    [SerializeField] private int _maxBusLevel = 3;

    [SerializeField] private float _minTimeBetweenCar = 4.5f;
    [SerializeField] private float _maxTimeBetweenCar = 9f;
    [SerializeField] private float _timeToWaitAvailableVehicle = 3f;
    [SerializeField] private int _defaultVehiclesCount = 30;
    
    private VehicleFactory _vehicleFactory;
    private VehicleStation _vehicleStation;

    public CityModel CityModel { get; private set; }
    public CancellationTokenSource CancellationTokenSource { get; private set; }

    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    public event Action<Vector2> CityConnected;

    public void Initialize(CityModel cityModel, VehicleFactory vehicleFactory)
    {
        _vehicleFactory = vehicleFactory;
        CityModel = cityModel;
        CancellationTokenSource = new CancellationTokenSource();
        _spriteRenderer.color = _provinceView.CitiesColor;
        gameObject.SetActive(false);

        _provinceView.ProvinceViewInitialized += OnProvinceInitialization;
        CityModel.CityConnected += OnCityConnect;
    }

    public void OnProvinceInitialization()
    {
        _provinceView.ProvinceViewInitialized -= OnProvinceInitialization;
        _provinceView.ProvinceModel.Activated += OnProvinceActivation;
    }

    public void OnCityConnect(CityModel connectedCity)
    {
        CityConnected?.Invoke(connectedCity.Position);
    }

    private void OnProvinceActivation()
    {
        gameObject.SetActive(true);
        _vehicleStation = new VehicleStation(this, _vehicleFactory, _provinceView.ProvinceModel.Level, _minTimeBetweenCar, _maxTimeBetweenCar, _timeToWaitAvailableVehicle, _defaultVehiclesCount);
        _provinceView.ProvinceViewInitialized -= OnProvinceActivation;
    }

    private void OnDestroy()
    {
        CityModel.CityConnected -= OnCityConnect;
        CancellationTokenSource?.Dispose();
    }
}

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

    public HashSet<CityModel> NearestConnectedCities { get; private set; } = new HashSet<CityModel>();
    public Vector2 Position { get; private set; }
    public ProvinceModel ProvinceModel => _provinceModel;
    public int CurrentBikeLevel { get; private set; } = 1;
    public int CurrentCarLevel { get; private set; } = 1;
    public int CurrentBusLevel { get; private set; } = 1;

    public event Action<CityModel> CityConnected;

    public CityModel(Vector2 position)
    {
        Position = position;
    }

    public void AddConnectedCity(CityModel connectedCityModel)
    {
        NearestConnectedCities.Add(connectedCityModel);
        CityConnected?.Invoke(connectedCityModel);
    }
}