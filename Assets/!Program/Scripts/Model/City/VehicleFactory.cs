using UnityEngine;

public class VehicleFactory : MonoBehaviour
{
    [SerializeField] private MotoBike _motoBike;

    private static MotoBike _motoBikeStatic;

    private const int FirstCityLevel = 1;
    //private const int SecondCityLevel = 2;
    //private const int ThirdCityLevel = 3;

    public void Initialize()
    {
        _motoBikeStatic = _motoBike;
    }

    public static Vehicle CreateVehicle(int cityLevel, Color cityColor, Transform parentTransform)
    {
        if (cityLevel == FirstCityLevel)
        {
            Vehicle vehicle = Instantiate(_motoBikeStatic, parentTransform);
            vehicle.Initialize(cityColor);
            vehicle.Disable();
            return vehicle;
        }

        return null;
    }
}