using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CitiesInitializer : MonoBehaviour
{
	private readonly List<CityView> _cities = new List<CityView>();

	// [Inject]
    private void InitializeAllCities(VehicleFactory _vehicleFactory)
	{
		_cities.AddRange(GetComponentsInChildren<CityView>());

		foreach(CityView city in _cities)
		{
			CityModel cityModel = new CityModel(city.transform.position, city.name);
            city.Initialize(cityModel, _vehicleFactory);
		}
	}
}