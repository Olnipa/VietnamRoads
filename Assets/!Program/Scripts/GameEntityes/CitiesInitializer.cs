using System.Collections.Generic;
using UnityEngine;

public class CitiesInitializer : MonoBehaviour
{
	private List<City> _cities = new List<City>();

    public void InitializeAllCities(VehicleFactory _vehicleFactory)
	{
		_cities.AddRange(GetComponentsInChildren<City>());

		foreach(City city in _cities)
		{
			city.Initialize(_vehicleFactory);
		}
	}
}