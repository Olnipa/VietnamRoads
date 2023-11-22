using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Road : MonoBehaviour
{
    public LineRenderer LineRenderer { get; private set; }
    public City FirstConnectedCity { get; private set; }
    public City SecondConnectedCity { get; private set; }

    public void Initialize(Vector2 startPosition, Vector2 endPosition, City firstCity, City secondCity)
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