using DG.Tweening;
using DG.Tweening.Core;
using System;
using UnityEngine;
using DG.Tweening.Plugins.Options;

public abstract class Vehicle : MonoBehaviour
{
    [SerializeField] protected float Speed = 1.5f;
    [SerializeField] protected SpriteRenderer SpriteRenderer;

    private Vector3 _targetPosition;

    private TweenerCore<Vector3, Vector3, VectorOptions> _tween;
    private int _currentPassengersCount;
    private int _startRegionLevel;

    protected int PassengersCapacity { get; private set; }

    public bool IsMoving { get; private set; }
    public bool IsBlocked { get; private set; } = true;

    public event Action<int, int> VehicleArrived;

    public void Initialize(Color color, int passengersCapacity)
    {
        SpriteRenderer.color = color;
        PassengersCapacity = passengersCapacity;
    }

    public void Move(Vector2 startPosition, Vector2 endPosition, int startRegionLevel)
    {
        Enable();
        _startRegionLevel = startRegionLevel;
        _currentPassengersCount = PassengersCapacity;
        transform.position = startPosition;
        _targetPosition = endPosition;
        Vector3 direction = _targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = rotation;

        _tween = transform
            .DOMove(_targetPosition, direction.magnitude / Speed)
            .SetEase(Ease.Linear)
            .OnComplete(StopMovement);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        IsMoving = false;
    }

    public void UnBlock()
    {
        IsBlocked = false;
    }

    private void OnDisable()
    {
        KillTweening();
    }

    private void StopMovement()
    {
        Disable();
        VehicleArrived.Invoke(_currentPassengersCount, _startRegionLevel);
    }

    private void Enable()
    {
        gameObject.SetActive(true);
        IsMoving = true;
    }

    private void KillTweening()
    {
        if (_tween != null && _tween.IsComplete() == false)
            _tween.Kill(true);
    }
}