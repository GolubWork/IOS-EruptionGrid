using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Physics.Systems
{
    public class SyncInitialRigidbodyPositionSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entitiesToSync;
        private List<GameEntity> _buffer = new (1);

        public SyncInitialRigidbodyPositionSystem(GameContext gameContext)
        {
            _entitiesToSync = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.PhysicsBody, 
                    GameMatcher.WorldPosition,
                    GameMatcher.Rigidbody2D
                    )
                .NoneOf(
                    GameMatcher.Initialized
                    ));
        }

        public void Execute()
        {
            foreach (var entity in _entitiesToSync.GetEntities(_buffer))
            {
                entity.Rigidbody2D.position = entity.WorldPosition;
                entity.isInitialized = true;
            }
        }
    }
}