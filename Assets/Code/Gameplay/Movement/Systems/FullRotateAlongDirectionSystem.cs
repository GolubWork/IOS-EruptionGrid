using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class FullRotateAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rotators;

        public FullRotateAlongDirectionSystem(GameContext gameContext)
        {
            _rotators = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Transform,
                GameMatcher.FullRotationAlignedAlongDirection,
                GameMatcher.Moving,
                GameMatcher.Direction
            ));
        }

        public void Execute()
        {
            foreach (GameEntity rotator in _rotators)
            {
                if (rotator.isMoving && rotator.Direction.sqrMagnitude >= 0.01f)
                {
                    float targetAngle = Mathf.Atan2(rotator.Direction.y, rotator.Direction.x) * Mathf.Rad2Deg;
                    rotator.Transform.rotation = Quaternion.Euler(0, 0, targetAngle);
                }
            }
        }
    }
}