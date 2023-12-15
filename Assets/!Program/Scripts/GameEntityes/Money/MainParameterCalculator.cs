using UnityEngine;

public class MainParameterCalculator
{
    private MainParameterModel _moneyModel;
    private MainParameterModel _passengerModel;

    public MainParameterCalculator(MainParameterModel moneyModel, MainParameterModel passengerModel)
    {
        _moneyModel = moneyModel;
        _passengerModel = passengerModel;
    }

    public void OnVehicleArrived(int passengersCount, int regionLevel)
    {
        int moneyToAdd = Mathf.RoundToInt(passengersCount * regionLevel * GameUpgrades.TicketPriceCoefficient);
        _moneyModel.AddValue(moneyToAdd);
        _passengerModel.AddValue(passengersCount);
    }
}