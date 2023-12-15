using System;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    private List<Province> Provincies = new List<Province>();
	private bool _isActive;

	public event Action Activated;

	public void Initialize(ProvinceLevelIncreaser provinceLevelCalculator)
	{
		Provincies.AddRange(GetComponentsInChildren<Province>());

		foreach (var province in Provincies)
		{
			province.Initialize(provinceLevelCalculator);
		}
	}

	public void Acivate()
	{
        _isActive = true;
		Activated?.Invoke();
    }
}