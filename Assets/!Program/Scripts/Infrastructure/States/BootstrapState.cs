using Zenject;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly MonoInstaller _monoInstaller;

    public BootstrapState(GameStateMachine stateMachine, MonoInstaller monoInstaller)
    {
      _stateMachine = stateMachine;
      _monoInstaller = monoInstaller;

      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _monoInstaller.gameObject.SetActive(true);
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadProgressState>();

    // private static IInputService InputService() =>
    //   Application.isEditor
    //     ? (IInputService) new StandaloneInputService()
    //     : new MobileInputService();
  }
}