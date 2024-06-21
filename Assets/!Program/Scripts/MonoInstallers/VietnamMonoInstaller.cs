using UnityEngine;
using Zenject;

public class VietnamMonoInstaller : MonoInstaller
{
  [SerializeField] private UniversalCloseButton _universalCloseButton;
  [SerializeField] private CityPanel _cityPanel;
  [SerializeField] private ProvinceUnlockPanel _provinceUnlockPanel;
  [SerializeField] private InputManager _inputManager;
  [SerializeField] private PassengerView _passengerView;
  [SerializeField] private MoneyView _moneyView;
  [SerializeField] private VehicleFactory _vehicleFactory;

  private readonly CompositeDisposable _compositeDisposable = new();

  public override void InstallBindings()
  {
    Container.Bind<PriceList>().AsSingle().NonLazy();
    Container.Bind<CompositeDisposable>().FromInstance(_compositeDisposable);
    Container.Bind<ProvinceLevelIncreaser>().AsSingle();
    Container.Bind<ProvinceUnlockPanel>().FromInstance(_provinceUnlockPanel);

    Container.Bind<MainParameterModel>().To<PassengersModel>().WhenInjectedInto<PassengerView>();
    Container.Bind<MainParameterModel>().To<MoneyModel>().WhenInjectedInto<MoneyView>();
    Container.Bind<MainParameterModel>().To<MoneyModel>().WhenInjectedInto<LinePositionSetter>();
    Container.Bind<MainParameterModel>().To<MoneyModel>().WhenInjectedInto<RoadBuilder>();

    Container.Bind<PassengersModel>().AsCached();
    Container.Bind<MoneyModel>().AsCached();

    MoneyModel a = Container.Resolve<MoneyModel>();
    var b = Container.Resolve<PassengersModel>();
    var c = Container.Resolve<ProvinceUnlockPanel>();

    Container.Bind<MainParametersCalculator>().AsSingle().WithArguments(a, b, c);

    Container.Bind<PassengerView>().FromInstance(_passengerView);
    Container.Bind<MoneyView>().FromInstance(_moneyView);

    Container.Bind<UniversalCloseButton>().FromInstance(_universalCloseButton);
    Container.Bind<CityPanel>().FromInstance(_cityPanel);
    Container.Bind<InputManager>().FromInstance(_inputManager);
    Container.Bind<VehicleFactory>().FromInstance(_vehicleFactory);

    Container.Bind<UniversalCloseButtonSwitcher>().AsSingle();
    Container.Bind<GameUpgrades>().AsSingle();
    Container.Bind<UIStateMachine>().AsSingle();
    Container.Bind<ClickedObjectDetector>().AsSingle();
    Container.Bind<CameraMoverSwitcher>().AsSingle();
    Container.Bind<UIPanelsSwitcher>().AsSingle();
  }

  private void OnDestroy()
  {
    _compositeDisposable.DisposeAll();
  }
}