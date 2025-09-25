using Code.Gameplay.Effects.Factory;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.EffectsVisual.Factories;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Entitas;

namespace Code.Gameplay.Collection.Systems
{
    public class CreateEffectOnCollectSystem: IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IVisualEffectFactory _visualEffectFactory;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IGroup<GameEntity> _collected;

        public CreateEffectOnCollectSystem(GameContext game, IEffectFactory effectFactory, IVisualEffectFactory visualEffectFactory, IVisualEffectStaticDataService visualEffectStaticDataService)
        {
            _effectFactory = effectFactory;
            _visualEffectFactory = visualEffectFactory;
            _visualEffectStaticDataService = visualEffectStaticDataService;
            _collected = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Id,
                GameMatcher.Collected
            ));
        }

        public void Execute()
        {
            foreach (GameEntity collected in _collected)
            {
                _effectFactory.CreateEffect(collected.CollectEffect, collected.Id, collected.Id);
                _visualEffectFactory.CreateVisualEffect(_visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.Collect), collected.Id, collected.Id, collected.WorldPosition);
            }
        }
    }
}