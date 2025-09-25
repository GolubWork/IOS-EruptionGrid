using System.Collections.Generic;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;

namespace Code.Gameplay.GameOver.Systems
{
    public class GameOverOnZeroGameResourceSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAudioFactory _audioFactory;

        public GameOverOnZeroGameResourceSystem(
            GameContext game, 
            IGameStateMachine stateMachine,
            IAudioFactory audioFactory) : base(game)
        {
            _stateMachine = stateMachine;
            _audioFactory = audioFactory;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.AllOf(
                GameMatcher.GameResource,
                GameMatcher.ResourceValue
            ));

        protected override bool Filter(GameEntity entity) => entity.ResourceValue == 0;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                if (entity.ResourceValue == 0)
                {
                    _audioFactory.CreateSound(SoundTypeId.Lose);
                    _stateMachine.Enter<GameOverState>();
                }
            }
        }
    }
}