using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LinePositionSetter : MonoBehaviour
{
    [SerializeField] private Color ConnectedLineColor = Color.green;
    [SerializeField] private Color DisconnectedLineColor = Color.white;

    private LineRenderer _lineRenderer;
    private CityDetector _cityDetector;

    private bool _isCityClicked;
    private bool _isCityConnected;

    public Vector2 StartLinePosition { get; private set; }
    public Vector2 EndLinePosition { get; private set; }

    private void FixedUpdate()
    {
        if (_isCityClicked == false)
            return;

        _isCityConnected = _cityDetector.TryGetConnectedCityPosition(out Vector2 position);
        EndLinePosition = position;
        DrawLine();

        if (_isCityConnected)
            SetLineColor(ConnectedLineColor);
        else
            SetLineColor(DisconnectedLineColor);
    }

    private void OnDestroy()
    {
        _cityDetector.CityClicked -= OnCityClicked;
        _cityDetector.DetectedCitiesReset -= OnCitiesReset;
    }

    public void Initialize(CityDetector cityDetector)
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _cityDetector = cityDetector;

        _cityDetector.CityClicked += OnCityClicked;
        _cityDetector.DetectedCitiesReset += OnCitiesReset;

        _lineRenderer.enabled = false;
        enabled = false;
    }

    private void SetLineColor(Color color)
    {
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }

    private void OnCityClicked(Vector2 clickedCityPosition)
    {
        enabled = true;
        StartLinePosition = clickedCityPosition;
        _lineRenderer.enabled = true;
        _isCityClicked = true;
    }

    private void OnCitiesReset()
    {
        if (_isCityClicked == false)
            return;

        _lineRenderer.enabled = false;
        _isCityClicked = false;
        enabled = false;
    }

    private void DrawLine()
    {
        _lineRenderer.SetPositions(new Vector3[] { StartLinePosition, EndLinePosition });
    }
}