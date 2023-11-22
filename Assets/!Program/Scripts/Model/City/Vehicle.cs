using DG.Tweening;
using System;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    [SerializeField] protected float _speed = 1.5f;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    
    private Vector3 _targetPosition;

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tween;

    public bool IsMoving { get; private set; }
    public bool IsBlocked { get; private set; } = true;

    private void OnDisable()
    {
        KillTweening();
    }

    public void Initialize(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void Move(Vector2 startPosition, Vector2 endPosition)
    {
        Enable();
        transform.position = startPosition;
        _targetPosition = endPosition;
        Vector3 direction = _targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = rotation;

        tween = transform.DOMove(_targetPosition, direction.magnitude / _speed)
            .SetEase(Ease.Linear)
            .OnComplete(Stop);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        IsMoving = false;
    }

    private void Stop()
    {
        Disable();
        KillTweening();
    }

    private void Enable()
    {
        gameObject.SetActive(true);
        IsMoving = true;
    }

    private void KillTweening()
    {
        if (tween != null && tween.IsComplete() == false)
            tween.Kill(true);
    }

    public void UnBlock()
    {
        IsBlocked = false;
    }
}
