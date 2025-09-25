using System.Collections.Generic;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;

namespace Code.Gameplay.GameOver.Systems
{
    public class GameOverOnZeroCardSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGameStateMachine _stateMachine;

        public GameOverOnZeroCardSystem(GameContext game, IGameStateMachine stateMachine) : base(game)
        {
            _stateMachine = stateMachine;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Card.Removed());

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            var remainingCards = Contexts.sharedInstance.game.GetGroup(GameMatcher.Card);
            if (remainingCards.GetEntities().Length == 0)
            {
                _stateMachine.Enter<GameWinState>();
            }
        }
    }
}