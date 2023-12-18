using System;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    private List<ProvinceView> _provincies = new List<ProvinceView>();
    private List<ProvinceLocker> _provinciesUnlockers = new List<ProvinceLocker>();
	private bool _isActive;

	public event Action Activated;

	public void Initialize(ProvinceLevelIncreaser provinceLevelCalculator, UIPanelsSwitcher uiPanelsSwitcher)
	{
		_provincies.AddRange(GetComponentsInChildren<ProvinceView>());
        _provinciesUnlockers.AddRange(GetComponentsInChildren<ProvinceLocker>());

		foreach (var province in _provincies)
		{
			province.Initialize(new ProvinceModel(provinceLevelCalculator));
        }

		foreach (var unlocker in _provinciesUnlockers)
		{
			unlocker.Initialize(uiPanelsSwitcher);
        }
	}

	public void Acivate()
	{
        _isActive = true;
		Activated?.Invoke();
    }
}