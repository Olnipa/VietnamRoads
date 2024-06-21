using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ClickedObjectDetector
{
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _rayCastResults = new RaycastHit2D[3];
    private Camera _camera;
    private InputManager _inputManager;

    private const string LayerMaskCity = "City";
    
    public CityView ClickedCity { get; private set; }
    public CityView ConnectedCity { get; private set; }

    public event Action<Vector2> CityClicked;
    public event Action CityConnected;
    public event Action<CityModel> CityChosed;
    public event Action DetectedCitiesReset;
    public event Action<ProvinceModel> ProvinceLockerClicked;

    public ClickedObjectDetector(InputManager inputManager)
    {
        _inputManager = inputManager;
        _camera = Camera.main;
        _contactFilter.useLayerMask = true;
        _contactFilter.useTriggers = true;
        LayerMask layerMask = LayerMask.GetMask(LayerMaskCity);
        _contactFilter.layerMask = layerMask;

        _inputManager.ButtonDownClicked += OnButtonDownClick;
        _inputManager.ButtonUpClicked += OnUpButtonClick;
        _inputManager.Destroyed += OnInputManagerDestroy;
    }

    public bool TryGetConnectedCityPosition(out Vector2 currentPosition)
    {
        Vector3 currentMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = currentMousePosition - ClickedCity.transform.position;
        int hitsCount = Physics2D.Raycast(ClickedCity.transform.position, direction, _contactFilter, _rayCastResults, direction.magnitude);

        if (hitsCount <= 1 || _rayCastResults[1].collider.TryGetComponent(out CityView city) == false)
        {
            ConnectedCity = null;
            currentPosition = currentMousePosition;
            return false;
        }

        ConnectedCity = city;
        currentPosition = ConnectedCity.transform.position;
        return true;
    }

    private CityView TryGetObjectUnderMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (var hit in hits)
        {
            if (hit == false)
                continue;

            if (hit.collider.TryGetComponent(out CityView clickedCity) && clickedCity.isActiveAndEnabled)
            {
                CityClicked.Invoke(clickedCity.transform.position);
                return clickedCity;
            }
            else if (hit.collider.TryGetComponent(out ProvinceLocker provinceLocker) && provinceLocker.isActiveAndEnabled)
            {
                ProvinceLockerClicked.Invoke(provinceLocker.ProvinceView.ProvinceModel);
            }

        }

        return null;
    }

    private void OnUpButtonClick()
    {
        if (ClickedCity != null)
        {
            if (ConnectedCity != null)
                CityConnected.Invoke();
            else if (ClickedCity == TryGetObjectUnderMouse())
                CityChosed.Invoke(ClickedCity.CityModel);
        }

        ClickedCity = null;
        ConnectedCity = null;
        DetectedCitiesReset.Invoke();
    }

    private void OnButtonDownClick()
    {
        ClickedCity = TryGetObjectUnderMouse();
    }

    private void OnInputManagerDestroy()
    {
        _inputManager.ButtonDownClicked -= OnButtonDownClick;
        _inputManager.ButtonUpClicked -= OnUpButtonClick;
        _inputManager.Destroyed -= OnInputManagerDestroy;
    }
}