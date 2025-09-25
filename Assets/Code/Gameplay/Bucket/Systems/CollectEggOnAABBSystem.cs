using System.Collections.Generic;
using Code.Gameplay.Common.AABB;
using Entitas;

namespace Code.Gameplay.Bucket.Systems
{
    public class CollectEggOnAABBSystem: IExecuteSystem
    {
        private readonly IAABBPhysicsService _aabbPhysicsService;
        private readonly IGroup<GameEntity> _buckets;
        private readonly IGroup<GameEntity> _eggs;
        private List<GameEntity> _buffer = new (12);

        public CollectEggOnAABBSystem(GameContext game, IAABBPhysicsService aabbPhysicsService)
        {
            _aabbPhysicsService = aabbPhysicsService;
            _eggs = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Egg,
                GameMatcher.SpriteRenderer,
                GameMatcher.Transform,
                GameMatcher.WorldPosition
            ));

            _buckets = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Bucket,
                GameMatcher.SpriteRenderer,
                GameMatcher.WorldPosition,
                GameMatcher.Transform
            ));
        }

        public void Execute()
        {
            foreach (GameEntity bucket in _buckets)
            foreach (GameEntity egg in _eggs.GetEntities(_buffer))
            {
                if (_aabbPhysicsService.IsColliding(bucket, egg))
                {
                    egg.isCollected = true;
                }
            }
        }
    }
}