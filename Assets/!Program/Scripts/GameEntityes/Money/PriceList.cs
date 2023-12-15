using System;
using UnityEngine;

public class PriceList : IDisposable
{
    private ProvinceLevelIncreaser _provinceLevelIncreaser;

    public static int PassengersToUnlockProvince { get; private set; } = 1000;
    public static float RoadUnitPrice { get; private set; } = 1.1f;
    public static int UnlockProvincePrice { get; private set; } = 100;

    public PriceList(ProvinceLevelIncreaser provinceLevelIncreaser)
    {
        _provinceLevelIncreaser = provinceLevelIncreaser;
        _provinceLevelIncreaser.LevelIncreased += OnProvinceUnlock;
    }

    public void OnProvinceUnlock()
    {
        UnlockProvincePrice = Mathf.RoundToInt(GameUpgrades.UnlockProvincePriceCoefficient * UnlockProvincePrice);
        PassengersToUnlockProvince = Mathf.RoundToInt(GameUpgrades.PassengersToNextLevelCoefficient * PassengersToUnlockProvince);
    }

    public void Dispose()
    {
        _provinceLevelIncreaser.LevelIncreased -= OnProvinceUnlock;
    }
}