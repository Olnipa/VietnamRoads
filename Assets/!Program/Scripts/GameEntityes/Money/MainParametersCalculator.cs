using System;
using UnityEngine;

public class MainParametersCalculator : IDisposable
{
    private MainParameterModel _moneyModel;
    private MainParameterModel _passengerModel;
    private ProvinceUnlockPanel _provinceUnlockPanel;

    public MainParametersCalculator(MainParameterModel moneyModel, MainParameterModel passengerModel, ProvinceUnlockPanel provinceUnlockPanel)
    {
        _moneyModel = moneyModel;
        _passengerModel = passengerModel;
        _provinceUnlockPanel = provinceUnlockPanel;

        _provinceUnlockPanel.OkButtonClicked += OnPovinceUnlockClick;
    }

    public void OnVehicleArrived(int passengersCount, int regionLevel)
    {
        int moneyToAdd = Mathf.RoundToInt(passengersCount * regionLevel * GameUpgrades.TicketPriceCoefficient);
        _moneyModel.AddValue(moneyToAdd);
        _passengerModel.AddValue(passengersCount);
    }

    public void OnPovinceUnlockClick(ProvinceModel provinceModel)
    {
        if (_moneyModel.TryRemoveValue(PriceList.UnlockNextProvincePrice))
            provinceModel.Unlock();
    }

    public void Dispose()
    {
        _provinceUnlockPanel.OkButtonClicked -= OnPovinceUnlockClick;
    }
}