using Code.Gameplay.Common.AABB;
using Code.Gameplay.Common.Physics;
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
    public class CollisionWithCardObstacleSystem: IExecuteSystem
    {
        private readonly IAABBPhysicsService _physicsService;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IEffectFactory _effectFactory;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IVisualEffectFactory _visualEffectFactory;
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<GameEntity> _cars;

        public CollisionWithCardObstacleSystem(
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
                GameMatcher.Transform,
                GameMatcher.BoxCollider2D
                ));
            _cars = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.CarObstacle,
                GameMatcher.WorldPosition,
                GameMatcher.SpriteRenderer,
                GameMatcher.Transform,
                GameMatcher.InitialeWorldPosition,
                GameMatcher.BoxCollider2D
            ));
        }

        public void Execute()
        {
            foreach(GameEntity player in _players)
            foreach (GameEntity car in _cars)
            {
                if(!_physicsService.IsBoxCollider2DColliding(player, car)) continue;
                
                EffectConfig config = _effectStaticDataService.GetEffectConfig(EffectTypeId.Damage);
                _effectFactory.CreateEffect(config, car.Id, player.Id);
                
                VisualEffectConfig visualConfig = _visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.TapEffect);
                _visualEffectFactory.CreateVisualEffect(visualConfig, car.Id, player.Id, player.WorldPosition);
                
                car.ReplaceWorldPosition(car.InitialeWorldPosition);
            }
        }
    }
}