using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTargetTransform;
    [SerializeField] private float _cameraMoveSpeed = 0.012f;
    [SerializeField] private PolygonCollider2D _cameraBoundsCollider;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private CameraMoverSwitcher _cameraMoverEnabler;
    private Vector3 _startMousePosition;
    private Vector3 _movementDelta;

    [Inject]
    private void Initialize(CameraMoverSwitcher cameraEnabler)
    {
        _cameraMoverEnabler = cameraEnabler;
        _cameraMoverEnabler.MovementStarted += OnCameraMoveStart;
        _cameraMoverEnabler.MovementStopped += OnCameraMoveStop;
        enabled = false;
    }

    private void OnDestroy()
    {
        _cameraMoverEnabler.MovementStarted -= OnCameraMoveStart;
        _cameraMoverEnabler.MovementStopped -= OnCameraMoveStop;
    }

    private void LateUpdate()
    {
        _movementDelta = _startMousePosition - Input.mousePosition;

        Vector3 newCameraTargetPosition = _cameraTargetTransform.position + 
            new Vector3(_movementDelta.x * _cameraMoveSpeed, _movementDelta.y * _cameraMoveSpeed, 0);

        _cameraTargetTransform.position = _cameraBoundsCollider.OverlapPoint(newCameraTargetPosition) ? 
            newCameraTargetPosition : _cameraBoundsCollider.ClosestPoint(newCameraTargetPosition);

        _startMousePosition = Input.mousePosition;
    }

    private void OnCameraMoveStart()
    {
        _startMousePosition = Input.mousePosition;
        enabled = true;
    }

    private void OnCameraMoveStop()
    {
        enabled = false;
    }
}
