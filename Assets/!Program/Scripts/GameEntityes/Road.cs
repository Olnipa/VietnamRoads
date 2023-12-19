using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Road : MonoBehaviour
{
    public LineRenderer LineRenderer { get; private set; }
    public CityModel FirstConnectedCity { get; private set; }
    public CityModel SecondConnectedCity { get; private set; }

    public void Initialize(Vector2 startPosition, Vector2 endPosition, CityModel firstCity, CityModel secondCity)
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.SetPositions(new Vector3[] { startPosition, endPosition });
        FirstConnectedCity = firstCity;
        SecondConnectedCity = secondCity;
        gameObject.SetActive(true);

        firstCity.AddConnectedCity(secondCity);
        secondCity.AddConnectedCity(firstCity);
    }
}