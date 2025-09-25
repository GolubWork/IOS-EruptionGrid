using Code.Common.Helpers;
using Code.Gameplay.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Taps.Systems
{
    public class CreateEffectOnDepletedSystem: IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _taped;

        public CreateEffectOnDepletedSystem(GameContext game, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _taped = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TapEffectConfig,
                GameMatcher.TapDepleted
            ));
        }

        public void Execute()
        {
            foreach (GameEntity taped in _taped)
            {
                _effectFactory.CreateEffect(taped.TapEffectConfig, taped.Id, taped.Id);
            }
        }
    }
}