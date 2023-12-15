using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class City : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ProvinceView _provinceView;

    [SerializeField] private int _defaultCarsCount = 3;
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

    public CancellationTokenSource CancellationTokenSource { get; private set; }
    public HashSet<City> NearestConnectedCities { get; private set; }  = new HashSet<City>();
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public ProvinceView ProvinceView => _provinceView;
    public int CurrentBikeLevel { get; private set; } = 1;
    public int CurrentCarLevel { get; private set; } = 1;
    public int CurrentBusLevel { get; private set; } = 1;

    public event Action<Vector2> CityConnected;

    public void Initialize(VehicleFactory vehicleFactory)
    {
        CancellationTokenSource = new CancellationTokenSource();
        _vehicleFactory = vehicleFactory;
        gameObject.SetActive(false);

        _provinceView.ProvinceViewInitialized += OnProvinceInitialization;
    }

    public void OnProvinceInitialization()
    {
        _provinceView.ProvinceViewInitialized -= OnProvinceInitialization;
        _provinceView.ProvinceModel.Activated += OnProvinceActivation;
    }

    public void IncreaseLevel()
    {
        if (CurrentBikeLevel < _maxMotorbikeLevel)
            CurrentBikeLevel++;
    }

    public void AddConnectedCity(City connectedCity)
    {
        NearestConnectedCities.Add(connectedCity);
        CityConnected?.Invoke(connectedCity.transform.position);
    }

    private void OnProvinceActivation()
    {
        gameObject.SetActive(true);
        _vehicleStation = new VehicleStation(this, _vehicleFactory, _provinceView.ProvinceModel.Level, _minTimeBetweenCar, _maxTimeBetweenCar, _timeToWaitAvailableVehicle, _defaultVehiclesCount);
        _provinceView.ProvinceViewInitialized -= OnProvinceActivation;
    }

    private void OnDestroy()
    {
        CancellationTokenSource?.Dispose();
    }
}