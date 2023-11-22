using System;
using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
	private bool _isActive;
    private List<City> _cities = new List<City>();

	public event Action Activated;

	public void  Initialize()
	{
		_cities.AddRange(GetComponentsInChildren<City>());

		foreach (var city in _cities)
		{
			city.Initialize();
		}
	}

	public void Activate()
	{
        _isActive = true;
        Activated?.Invoke();
    }
}