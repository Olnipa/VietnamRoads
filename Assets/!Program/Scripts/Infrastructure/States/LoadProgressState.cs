namespace CodeBase.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _gameStateMachine;

    public LoadProgressState(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();
      
      // _gameStateMachine.Enter<LoadLevelState, string>();
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
      // _progressService.Progress = 
      //   _saveLoadProgress.LoadProgress() 
      //   ?? NewProgress();
    }

    // private PlayerProgress NewProgress()
    // {
    //   var progress =  new PlayerProgress(initialLevel: "Main");
    //
    //   return progress;
    // }
  }
}