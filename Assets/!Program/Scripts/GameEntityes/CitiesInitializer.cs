using System.Collections.Generic;
using UnityEngine;

public class CitiesInitializer : MonoBehaviour
{
	private List<CityView> _cities = new List<CityView>();

    public void InitializeAllCities(VehicleFactory _vehicleFactory)
	{
		_cities.AddRange(GetComponentsInChildren<CityView>());

		foreach(CityView city in _cities)
		{
			CityModel cityModel = new CityModel(city.transform.position, city.name);
            city.Initialize(cityModel, _vehicleFactory);
		}
	}
}