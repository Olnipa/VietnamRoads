using UnityEngine;
using Zenject;

public class VehicleFactory : MonoBehaviour
{
    [SerializeField] private MotoBike _motorbike;
    [SerializeField] private int _motorbikeInitialCapacity = 1;

    private MainParametersCalculator _moneyCalculator;

    private const int FirstCityLevel = 1;
    //private const int SecondCityLevel = 2;
    //private const int ThirdCityLevel = 3;

    [Inject]
    private void Initialize(MainParametersCalculator moneyCalculator)
    {
        _moneyCalculator = moneyCalculator;
    }

    public Vehicle CreateVehicle(int cityLevel, Color cityColor, Transform parentTransform)
    {
        if (cityLevel == FirstCityLevel)
        {
            Vehicle vehicle = Instantiate(_motorbike, parentTransform);
            vehicle.Initialize(cityColor, _motorbikeInitialCapacity);
            vehicle.VehicleArrived += OnVehicleArrived;
            vehicle.Disable();
            return vehicle;
        }

        return null;
    }

    private void OnVehicleArrived(int passengersCount, int regionLevel)
    {
        _moneyCalculator.OnVehicleArrived(passengersCount, regionLevel);
    }
}
