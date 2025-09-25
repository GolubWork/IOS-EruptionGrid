using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Physics.Systems
{
    public class UpdateWorldPositionByRigidbody2DPosition : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateWorldPositionByRigidbody2DPosition(GameContext gameContext)
        {
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.PhysicsBody,
                    GameMatcher.WorldPosition,
                    GameMatcher.Rigidbody2D
                )
            );
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                mover.ReplaceWorldPosition(mover.Rigidbody2D.position);
            }
        }
    }
}