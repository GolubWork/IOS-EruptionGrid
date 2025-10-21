using Code.Gameplay.Common.AABB;
using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Configs;
using Code.Gameplay.Effects.Factory;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.EffectsVisual.Factories;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Entitas;

namespace Code.Gameplay.Obstacles.Systems
{
    public class CollisionWithCurrencyObstacle : IExecuteSystem
    {
        private readonly IAABBPhysicsService _physicsService;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IEffectFactory _effectFactory;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IVisualEffectFactory _visualEffectFactory;
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<GameEntity> _cars;

        public CollisionWithCurrencyObstacle(
            GameContext context,
            IAABBPhysicsService physicsService,
            IEffectStaticDataService effectStaticDataService,
            IEffectFactory effectFactory,
            IVisualEffectStaticDataService visualEffectStaticDataService,
            IVisualEffectFactory visualEffectFactory)
        {
            _physicsService = physicsService;
            _effectStaticDataService = effectStaticDataService;
            _effectFactory = effectFactory;
            _visualEffectStaticDataService = visualEffectStaticDataService;
            _visualEffectFactory = visualEffectFactory;
            _players = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.WorldPosition,
                GameMatcher.SpriteRenderer,
                GameMatcher.Transform
            ));
            _cars = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.CurrencyObstacle,
                GameMatcher.WorldPosition,
                GameMatcher.SpriteRenderer,
                GameMatcher.Transform
            ));
        }

        public void Execute()
        {
            foreach (GameEntity player in _players)
            foreach (GameEntity car in _cars)
            {
                if (!_physicsService.IsColliding(player, car)) continue;
                EffectConfig config = _effectStaticDataService.GetEffectConfig(EffectTypeId.AddCurrency);
                _effectFactory.CreateEffect(config, car.Id, player.Id);

                VisualEffectConfig visualConfig =
                    _visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.Collect);
                _visualEffectFactory.CreateVisualEffect(visualConfig, car.Id, player.Id, player.WorldPosition);

                car.isDestructed = true;
            }
        }
    }
}