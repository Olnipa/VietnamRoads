using UnityEngine;
using UnityEngine.UI;

public class BootStrap : MonoBehaviour
{
    // [SerializeField] private LinePositionSetter _linePositionSetter;
    // [SerializeField] private RoadBuilder _roadBuilder;
    // [SerializeField] private InputManager _inputManager;
    // [SerializeField] private VehicleFactory _vehicleFactory;
    // [SerializeField] private Country _country;
    // [SerializeField] private CameraMover _cameraMover;
    //
    // [SerializeField] private MainParameterView _moneyView;
    // [SerializeField] private MainParameterView _passengerView;
    //
    // [SerializeField] private CityPanel _cityPanel;
    // [SerializeField] private ProvinceUnlockPanel _provinceUnlockPanel;
    // [SerializeField] private CitiesInitializer _citiesInitializer;
    // [SerializeField] private UniversalCloseButton _universalCloseButton;
    
    private MainParameterModel _moneyModel;
    private MainParameterModel _passengersModel;

    private MainParametersCalculator _mainParametersCalculator;
    private UIPanelsSwitcher _uiPanelSwitcher;
    private CameraMoverSwitcher _cameraMoverSwitcher;
    private ClickedObjectDetector _cityDetector;
    private UIStateMachine _uiStateMachine;
    //private CompositeDisposable _compositeDisposable;
    //private ProvinceLevelIncreaser _provinceLevelIncreaser;
    private GameUpgrades _gameUpgrades;
    //private PriceList _priceList;
    private UniversalCloseButtonSwitcher _universalCloseButtonSwitcher;

    private void Start()
    {
        //_compositeDisposable = new CompositeDisposable();

        //_provinceLevelIncreaser = new ProvinceLevelIncreaser();
        //_priceList = new PriceList(_provinceLevelIncreaser, _compositeDisposable);

        //_passengersModel = new PassengersModel();
        //_passengerView.Initialize(_passengersModel);

        //_moneyModel = new MoneyModel();
        //_moneyView.Initialize(_moneyModel);

        //_cityPanel.Initialize();
        //_provinceUnlockPanel.Initialize();
        //_universalCloseButtonSwitcher = new UniversalCloseButtonSwitcher(_universalCloseButton, _cityPanel, _provinceUnlockPanel);

        //_gameUpgrades = new GameUpgrades();
        //_mainParametersCalculator = new MainParametersCalculator(_moneyModel, _passengersModel, _provinceUnlockPanel);
        
        //_vehicleFactory.Initialize(_mainParametersCalculator);
        //_citiesInitializer.InitializeAllCities(_vehicleFactory);

        //_uiStateMachine = new UIStateMachine();
        //_cityDetector = new ClickedObjectDetector(_inputManager);
        //_cameraMoverSwitcher = new CameraMoverSwitcher(_inputManager, _cityDetector);

        //_roadBuilder.Initialize(_cityDetector, _moneyModel);

        //_uiPanelSwitcher = new UIPanelsSwitcher(_cityDetector, _uiStateMachine, _cityPanel, _provinceUnlockPanel, _universalCloseButtonSwitcher, _cameraMoverSwitcher);
        
        //_country.Initialize(_provinceLevelIncreaser, _uiPanelSwitcher);

        //_linePositionSetter.Initialize(_cityDetector, _moneyModel);
        //_cameraMover.Initialize(_cameraMoverSwitcher);

        //_compositeDisposable.Add(_priceList);
        //_compositeDisposable.Add(_universalCloseButtonSwitcher);
        //_compositeDisposable.Add(_mainParametersCalculator);
        //_compositeDisposable.Add(_uiPanelSwitcher);
    }

    private void OnDestroy()
    {
        //_compositeDisposable.DisposeAll();
    }
}