using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class VehicleStation
{
    private float _minTimeBetweenCarMove;
    private float _maxTimeBetweenCarMove;
    private float _timeToWaitAvailableVehicle;
    private float _vehiclesCountToUnblock;
    private int _provinceLevel;
    private VehicleFactory _vehicleFactory;
    private bool _isWorking;

    private List<Vehicle> _vehicles = new List<Vehicle>();

    public CityView RelatedCity { get; private set; }

    public VehicleStation(CityView city, VehicleFactory vehicleFactory, int provinceLevel, float minTimeBetweenCarMove = 2.5f, float maxTimeBetweenCarMove = 7f, 
        float timeToWaitAvailableVehicle = 3f, int defaultVehiclesCount = 25, float vehiclesCountToUnblock = 3)
    {
        RelatedCity = city;
        _vehicleFactory = vehicleFactory;
        _provinceLevel = provinceLevel;
        _minTimeBetweenCarMove = minTimeBetweenCarMove;
        _maxTimeBetweenCarMove = maxTimeBetweenCarMove;
        _timeToWaitAvailableVehicle = timeToWaitAvailableVehicle;
        _vehiclesCountToUnblock = vehiclesCountToUnblock;

        RelatedCity.CityConnected += OnNewCityConnected;

        IncreaseCountOfVehicles(defaultVehiclesCount);
    }

    public void OnNewCityConnected(Vector2 endPositioon)
    {
        _isWorking = true;
        UnblockVehicles(_vehiclesCountToUnblock);
        StartVehiclesMovement(endPositioon);
    }

    private void UnblockVehicles(float vehiclesCountToUnblock)
    {
        int unlockedCount = 0;

        foreach (var vehicle in _vehicles)
        {
            if (unlockedCount >= vehiclesCountToUnblock)
                break;

            if (vehicle.IsBlocked)
            {
                vehicle.UnBlock();
                unlockedCount++;
            }
        }
    }

    private void IncreaseCountOfVehicles(int countOfVehiclesToCreate, int cityLlevel = 1)
    {
        for (int i = 0; i < countOfVehiclesToCreate; i++)
        {
            Vehicle vehicle = _vehicleFactory.CreateVehicle(cityLlevel, RelatedCity.SpriteRenderer.color, RelatedCity.transform);

            if (vehicle != null)
                _vehicles.Add(vehicle);
        }
    }

    private async void StartVehiclesMovement(Vector2 endPosition)
    {
        Vehicle vehicle;

        while (_isWorking)
        {
            float delay = Random.Range(_minTimeBetweenCarMove, _maxTimeBetweenCarMove);

            await WaitAsync(delay).AttachExternalCancellation(RelatedCity.CancellationTokenSource.Token);
            vehicle = await GetAvailableVehicleAsync().AttachExternalCancellation(RelatedCity.CancellationTokenSource.Token);
            vehicle.Move(RelatedCity.transform.position, endPosition, _provinceLevel);
        }
    }

    private async UniTask WaitAsync(float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), ignoreTimeScale: false, PlayerLoopTiming.Update, RelatedCity.CancellationTokenSource.Token);
    }

    private async UniTask<Vehicle> GetAvailableVehicleAsync()
    {
        Vehicle vehicle = _vehicles.FirstOrDefault(vehicle => vehicle.IsMoving == false && vehicle.IsBlocked == false);

        if (vehicle != null)
            return vehicle;

        while (vehicle == null)
        {
            await WaitAsync(_timeToWaitAvailableVehicle).AttachExternalCancellation(RelatedCity.CancellationTokenSource.Token);
            vehicle = _vehicles.FirstOrDefault(vehicle => vehicle.IsMoving == false && vehicle.IsBlocked == false);
        }

        return vehicle;
    }
}