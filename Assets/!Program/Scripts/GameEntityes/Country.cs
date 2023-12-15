using System;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    private List<ProvinceView> Provincies = new List<ProvinceView>();
	private bool _isActive;

	public event Action Activated;

	public void Initialize(ProvinceLevelIncreaser provinceLevelCalculator)
	{
		Provincies.AddRange(GetComponentsInChildren<ProvinceView>());

		foreach (var province in Provincies)
		{
			province.Initialize(new ProvinceModel(provinceLevelCalculator));
		}
	}

	public void Acivate()
	{
        _isActive = true;
		Activated?.Invoke();
    }
}