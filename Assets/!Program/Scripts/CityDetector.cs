using System;
using UnityEngine;

public class CityDetector
{
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _rayCastResults = new RaycastHit2D[3];
    private Camera _camera;
    private InputManager _inputManager;

    private const string LayerMaskCity = "City";
    
    public City ClickedCity { get; private set; }
    public City ConnectedCity { get; private set; }

    public event Action<Vector2> CityClicked;
    public event Action CityConnected;
    public event Action CityChosed;
    public event Action DetectedCitiesReset;

    public CityDetector(InputManager inputManager)
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

    private void OnButtonDownClick()
    {
        ClickedCity = TryGetCityUnderMouse();
    }

    private City TryGetCityUnderMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (var hit in hits)
        {
            if (hit && hit.collider.TryGetComponent(out City clickedCity))
            {
                CityClicked.Invoke(clickedCity.transform.position);
                return clickedCity;
            }
        }

        return null;
    }

    public bool TryGetConnectedCityPosition(out Vector2 currentPosition)
    {
        Vector3 currentMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = currentMousePosition - ClickedCity.transform.position;
        int hitsCount = Physics2D.Raycast(ClickedCity.transform.position, direction, _contactFilter, _rayCastResults, direction.magnitude);

        if (hitsCount <= 1 || _rayCastResults[1].collider.TryGetComponent(out City city) == false)
        {
            ConnectedCity = null;
            currentPosition = currentMousePosition;
            return false;
        }

        ConnectedCity = city;
        currentPosition = ConnectedCity.transform.position;
        return true;
    }

    public void OnUpButtonClick()
    {
        if (ClickedCity != null)
        {
            if (ConnectedCity != null)
                CityConnected.Invoke();
            else if (ClickedCity == TryGetCityUnderMouse())
                CityChosed.Invoke();
        }

        ClickedCity = null;
        ConnectedCity = null;
        DetectedCitiesReset.Invoke();
    }

    private void OnInputManagerDestroy()
    {
        _inputManager.ButtonDownClicked -= OnButtonDownClick;
        _inputManager.ButtonUpClicked -= OnUpButtonClick;
        _inputManager.Destroyed -= OnInputManagerDestroy;
    }
}