using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private LinePositionSetter _linePositionSetter;
    [SerializeField] private RoadBuilder _roadBuilder;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private VehicleFactory _vehicleFactory;
    [SerializeField] private Country _country;
    [SerializeField] private CameraMover _cameraMover;

    [SerializeField] private CityPanel _cityPanel;
    
    private UIPanelsSwitcher _uiPanelSwitcher;
    private CameraMoverSwitcher _cameraMoverSwitcher;
    private CityDetector _cityDetector;
    private UIStateMachine _uiStateMachine;


    private void Awake()
    {
        _uiStateMachine = new UIStateMachine();
        _cityDetector = new CityDetector(_inputManager);
        _roadBuilder.Initialize(_cityDetector);
        _vehicleFactory.Initialize();
        _country.Initialize();

        _uiPanelSwitcher = new UIPanelsSwitcher(_cityDetector, _uiStateMachine, _cityPanel);

        _linePositionSetter.Initialize(_cityDetector);

        _cameraMoverSwitcher = new CameraMoverSwitcher(_inputManager, _cityDetector);
        _cameraMover.Initialize(_cameraMoverSwitcher);
    }
}