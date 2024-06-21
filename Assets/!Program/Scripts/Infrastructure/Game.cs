using System;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(LoadingCurtain curtain)
    {
      StateMachine = new GameStateMachine(curtain);
    }
  }
}