using System;
using UnityEngine;

public class PriceList : IDisposable
{
    private readonly ProvinceLevelIncreaser _provinceLevelIncreaser;

    public static int PassengersToAllowBuyingNextProvince { get; private set; } = 1000;
    public static float RoadUnitPrice { get; private set; } = 1.1f;
    public static int UnlockNextProvincePrice { get; private set; } = 1;

    public PriceList(ProvinceLevelIncreaser provinceLevelIncreaser, CompositeDisposable compositeDisposable)
    {
        _provinceLevelIncreaser = provinceLevelIncreaser;
        _provinceLevelIncreaser.LevelIncreased += OnProvinceUnlock;
        compositeDisposable.Add(this);
    }

    public void OnProvinceUnlock()
    {
        UnlockNextProvincePrice = Mathf.RoundToInt(GameUpgrades.UnlockProvincePriceCoefficient * UnlockNextProvincePrice);
        PassengersToAllowBuyingNextProvince = Mathf.RoundToInt(GameUpgrades.PassengersToNextLevelCoefficient * PassengersToAllowBuyingNextProvince);
    }

    public void Dispose()
    {
        _provinceLevelIncreaser.LevelIncreased -= OnProvinceUnlock;
    }
}