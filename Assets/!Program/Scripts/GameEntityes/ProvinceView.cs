using System;
using System.Collections.Generic;
using UnityEngine;

public class ProvinceView : View
{
  [SerializeField] private bool _isActive;
  [SerializeField] private SpriteRenderer _spriteRenderer;
  [SerializeField] private Color _citiesColor;

  private readonly List<CityView> _cities = new List<CityView>();

  public bool IsActive => _isActive;
  public Color CitiesColor => _citiesColor;

  public event Action ProvinceViewInitialized;

  public ProvinceModel ProvinceModel { get; private set; }

  public void Initialize(ProvinceModel provinceModel, VehicleFactory vehicleFactory)
  {
    InitializeCities(vehicleFactory);

    ProvinceModel = provinceModel;
    ProvinceModel.Activated += OnProvinceUnlock;
    
    ProvinceViewInitialized.Invoke();

    if (_isActive)
      ProvinceModel.Unlock();
    else
      _spriteRenderer.enabled = false;
  }

  private void InitializeCities(VehicleFactory vehicleFactory)
  {
    _cities.AddRange(GetComponentsInChildren<CityView>());

    foreach (CityView city in _cities)
    {
      CityModel cityModel = new CityModel(city.transform.position, city.name);
      city.Initialize(cityModel, vehicleFactory);
    }
  }

  private void OnProvinceUnlock()
  {
    gameObject.SetActive(true);
    _spriteRenderer.enabled = true;

    ProvinceModel.Activated -= OnProvinceUnlock;
  }
}