using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Country : MonoBehaviour
{
    private readonly List<ProvinceView> _provincies = new List<ProvinceView>();
    private readonly List<ProvinceLocker> _provinciesUnlockers = new List<ProvinceLocker>();
	private bool _isActive;

	public event Action Activated;

	[Inject]
	private void Initialize(ProvinceLevelIncreaser provinceLevelCalculator, UIPanelsSwitcher uiPanelsSwitcher,
		VehicleFactory vehicleFactory)
	{
		_provincies.AddRange(GetComponentsInChildren<ProvinceView>());
        _provinciesUnlockers.AddRange(GetComponentsInChildren<ProvinceLocker>());

		foreach (var province in _provincies)
		{
			province.Initialize(new ProvinceModel(provinceLevelCalculator), vehicleFactory);
        }

		foreach (var provinceLocker in _provinciesUnlockers)
		{
			provinceLocker.Initialize();
        }
	}

	public void Acivate()
	{
        _isActive = true;
		Activated?.Invoke();
    }
}