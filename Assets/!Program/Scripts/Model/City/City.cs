using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class City : MonoBehaviour
{
    [SerializeField] private int _defaultCarsCount = 3;
    [SerializeField] private int _additionalCarsOnLevelUp = 3;
    [SerializeField] private int _maxMotorbikeLevel = 3;
    [SerializeField] private int _maxCarLevel = 3;
    [SerializeField] private int _maxBusLevel = 3;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _minTimeBetweenCarMove = 4.5f;
    [SerializeField] private float _maxTimeBetweenCarMove = 9f;
    [SerializeField] private float _timeToWaitAvailableVehicle = 3f;
    [SerializeField] private int _defaultVehiclesCount = 30;

    private VehicleStation _vehicleStation;
    private CancellationTokenSource _cityDisableCancellation;

    public HashSet<City> NearestConnectedCities { get; private set; }  = new HashSet<City>();
    public int CurrentBikeLevel { get; private set; } = 1;
    public int CurrentCarLevel { get; private set; } = 1;
    public int CurrentBusLevel { get; private set; } = 1;

    public void Initialize()
    {
        _cityDisableCancellation = new CancellationTokenSource();
        _vehicleStation = new VehicleStation(_cityDisableCancellation.Token, _spriteRenderer.color, transform,
        _minTimeBetweenCarMove, _maxTimeBetweenCarMove, _timeToWaitAvailableVehicle, _defaultVehiclesCount);
    }

    public void IncreaseLevel()
    {
        if (CurrentBikeLevel < _maxMotorbikeLevel)
            CurrentBikeLevel++;
    }

    public void AddConnectedCity(City connectedCity)
    {
        NearestConnectedCities.Add(connectedCity);
        _vehicleStation.OnNewCityConnected(connectedCity.transform.position);
    }

    private void OnDestroy()
    {
        _cityDisableCancellation?.Dispose();
    }
}