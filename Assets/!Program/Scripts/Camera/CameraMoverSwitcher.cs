using System;

public class CameraMoverSwitcher
{
    private InputManager _inputManager;
    private ClickedObjectDetector _cityDetector;

    private bool _isAvailable = true;

    public event Action MovementStarted;
    public event Action MovementStopped;

    public CameraMoverSwitcher(InputManager inputManager, ClickedObjectDetector cityDetector)
    {
        _cityDetector = cityDetector;
        _inputManager = inputManager;

        _inputManager.ButtonDownClicked += TryEnableCameraMove;
        _inputManager.ButtonUpClicked += OnStopCameraMove;
        _inputManager.Destroyed += OnDestroy;
    }

    public void Disable()
    {
        _isAvailable = false;
    }

    public void Enable()
    {
        _isAvailable = true;
    }

    private void OnDestroy()
    {
        _inputManager.ButtonDownClicked -= TryEnableCameraMove;
        _inputManager.ButtonUpClicked -= OnStopCameraMove;
        _inputManager.Destroyed -= OnDestroy;
    }

    private void TryEnableCameraMove()
    {
        if (_isAvailable && _cityDetector.ClickedCity == null)
            MovementStarted.Invoke();
    }

    private void OnStopCameraMove()
    {
        MovementStopped.Invoke();
    }
}