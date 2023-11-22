using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class VehicleStation
{
    private float _minTimeBetweenCarMove;
    private float _maxTimeBetweenCarMove;
    private float _timeToWaitAvailableVehicle;
    private float _vehiclesCountToUnblock;

    private CancellationToken _cityDisableCancellation;
    private Color _cityColor;
    private Transform _cityTransform;

    private List<Vehicle> _vehicles = new List<Vehicle>();
    private bool _isWorking;

    public VehicleStation(CancellationToken cityDisableCancellation, Color cityColor, Transform cityTransform, float minTimeBetweenCarMove = 2.5f, 
        float maxTimeBetweenCarMove = 7f, float timeToWaitAvailableVehicle = 3f, int defaultVehiclesCount = 25, float vehiclesCountToUnblock = 3)
    {
        _cityDisableCancellation = cityDisableCancellation;
        _cityColor = cityColor;
        _cityTransform = cityTransform;
        _minTimeBetweenCarMove = minTimeBetweenCarMove;
        _maxTimeBetweenCarMove = maxTimeBetweenCarMove;
        _timeToWaitAvailableVehicle = timeToWaitAvailableVehicle;
        _vehiclesCountToUnblock = vehiclesCountToUnblock;

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
            Vehicle vehicle = VehicleFactory.CreateVehicle(cityLlevel, _cityColor, _cityTransform);

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

            await WaitAsync(delay).AttachExternalCancellation(_cityDisableCancellation);
            vehicle = await GetAvailableVehicleAsync().AttachExternalCancellation(_cityDisableCancellation);
            vehicle.Move(_cityTransform.position, endPosition);
        }
    }

    private async UniTask WaitAsync(float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), ignoreTimeScale: false, PlayerLoopTiming.Update, _cityDisableCancellation);
    }

    private async UniTask<Vehicle> GetAvailableVehicleAsync()
    {
        Vehicle vehicle = _vehicles.FirstOrDefault(vehicle => vehicle.IsMoving == false && vehicle.IsBlocked == false);

        if (vehicle != null)
            return vehicle;

        while (vehicle == null)
        {
            await WaitAsync(_timeToWaitAvailableVehicle).AttachExternalCancellation(_cityDisableCancellation);
            vehicle = _vehicles.FirstOrDefault(vehicle => vehicle.IsMoving == false && vehicle.IsBlocked == false);
        }

        return vehicle;
    }
}