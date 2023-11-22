using System;

public class CameraMoverSwitcher
{
    private InputManager _inputManager;
    private CityDetector _cityDetector;

    public event Action MovementStarted;
    public event Action MovementStopped;

    public CameraMoverSwitcher(InputManager inputManager, CityDetector cityDetector)
    {
        _cityDetector = cityDetector;
        _inputManager = inputManager;

        _inputManager.ButtonDownClicked += TryEnableCameraMove;
        _inputManager.ButtonUpClicked += OnStopCameraMove;
        _inputManager.Destroyed += OnDestroy;
    }

    private void OnDestroy()
    {
        _inputManager.ButtonDownClicked -= TryEnableCameraMove;
        _inputManager.ButtonUpClicked -= OnStopCameraMove;
        _inputManager.Destroyed -= OnDestroy;
    }

    private void TryEnableCameraMove()
    {
        if (_cityDetector.ClickedCity == null)
            MovementStarted.Invoke();
    }

    private void OnStopCameraMove()
    {
        MovementStopped.Invoke();
    }
}
