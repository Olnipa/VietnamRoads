using UnityEngine;
using DG.Tweening;

internal class Car : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private Color _color = Color.white;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        _startPosition = transform.position;
        _targetPosition = _targetTransform.position;
        Vector3 direction = _targetPosition - _startPosition;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = rotation;
        transform.DOMove(_targetPosition, 2).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetColor(Color newColor)
    {
        _color = newColor;
    }
}