using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Factory;
using Entitas;

namespace Code.Gameplay.EffectApplication.Systems
{
    public class ApplyEffectsOnTargetsSystem: IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _entities;

        public ApplyEffectsOnTargetsSystem(GameContext gameContext, IEffectFactory effectFactory)
        {
            _gameContext = gameContext;
            _effectFactory = effectFactory;
            _entities = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetsBuffer,
                GameMatcher.EffectSetups));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            foreach (int targetsId in entity.TargetsBuffer)
            foreach (EffectSetup setup in entity.EffectSetups)
            {
                _effectFactory.CreateEffect(setup, ProducerId(entity), targetsId);
            }
        }

        private static int ProducerId(GameEntity entity)
        {
            return entity.hasProducerId ? entity.ProducerId : entity.Id;
        }
        
    }
    
}