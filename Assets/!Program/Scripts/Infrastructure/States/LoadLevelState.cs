namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      // _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
      InitGameWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
      // foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
      //   progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
      InitHud();
    }

    private void InitHud()
    {
      // GameObject hud = _gameFactory.CreateHud();
      
      // hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
    }
  }
}