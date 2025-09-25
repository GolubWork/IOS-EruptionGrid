using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Physics.Systems
{
    public class ApplyForceSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _forces;
        private readonly IGroup<GameEntity> _physicsBodies;
        private List<GameEntity> _buffer = new List<GameEntity>(12);

        public ApplyForceSystem(GameContext gameContext)
        {
            _forces = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.ForceApplier,
                GameMatcher.PhysicsForce,
                GameMatcher.ForceTargetId,
                GameMatcher.ForceProducerId
            ));

            _physicsBodies = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.PhysicsBody,
                    GameMatcher.Rigidbody2D
                    ));
        }

        public void Execute()
        {
            foreach (GameEntity force in _forces.GetEntities(_buffer))
            foreach (GameEntity physicsBody in _physicsBodies)
            {
                int bodyId = physicsBody.Id;
                if (force.ForceTargetId == bodyId)
                {
                    physicsBody.Rigidbody2D.AddForce(force.PhysicsForce, ForceMode2D.Impulse);
                    force.isProcessed = true;
                }
            }
        }
    }
}