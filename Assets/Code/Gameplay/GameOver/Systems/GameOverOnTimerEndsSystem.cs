using System.Collections.Generic;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;

namespace Code.Gameplay.GameOver.Systems
{
  public class GameOverOnTimerEndsSystem : ReactiveSystem<GameEntity>
  {
    private readonly IGameStateMachine _stateMachine;

    public GameOverOnTimerEndsSystem(GameContext game, IGameStateMachine stateMachine) : base(game)
    {
      _stateMachine = stateMachine;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
      context.CreateCollector(GameMatcher
        .AllOf(
          GameMatcher.LevelTimer,
          GameMatcher.TimerCompleted)
        .Added());

    protected override bool Filter(GameEntity levelTimer) => levelTimer.isTimerCompleted;

    protected override void Execute(List<GameEntity> levelTimers)
    {
      _stateMachine.Enter<GameOverState>();
    }
  }
}