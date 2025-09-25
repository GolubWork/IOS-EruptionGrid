using System.Collections.Generic;
using Code.Gameplay.Common.AABB;
using Entitas;

namespace Code.Gameplay.Shelfs.Systems
{
    public class MarkOnShelfSystem: IExecuteSystem
    {
        private readonly IAABBPhysicsService _aabbPhysicsService;
        private readonly IGroup<GameEntity> _grabable;
        private readonly IGroup<GameEntity> _shelfs;
        private List<GameEntity> _buffer = new (1);

        public MarkOnShelfSystem(GameContext game, IAABBPhysicsService aabbPhysicsService)
        {
            _aabbPhysicsService = aabbPhysicsService;
            _grabable = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grabable,
                GameMatcher.WorldPosition,
                GameMatcher.SpriteRenderer,
                GameMatcher.Transform
            ).NoneOf(GameMatcher.OnShelf,
                GameMatcher.Grabed));

            _shelfs = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Shelf,
                GameMatcher.SpriteRenderer,
                GameMatcher.WorldPosition,
                GameMatcher.Transform
            ));
        }

        public void Execute()
        {
            foreach (GameEntity grabed in _grabable.GetEntities(_buffer))
            foreach (GameEntity shelf in _shelfs)
            {
                if (_aabbPhysicsService.IsColliding(grabed, shelf))
                {
                    grabed.isEggSpawnActive = true;
                    grabed.isOnShelf = true;
                }
                else
                {
                    grabed.isEggSpawnActive = true;
                    grabed.isOnShelf = false;
                }
            }
        }
    }
}