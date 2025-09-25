using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class RotateAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rotators;

        public RotateAlongDirectionSystem(GameContext gameContext)
        {
            _rotators = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Transform,
                GameMatcher.RotationAlignedAlongDirection,
                GameMatcher.Direction
            ));
        }

        public void Execute()
        {
            foreach (GameEntity rotator in _rotators)
            {
                if (rotator.Direction.sqrMagnitude >= 0.01f)
                {
                    float turnAngle = Mathf.Atan2(rotator.Direction.y, rotator.Direction.x) * Mathf.Rad2Deg;
                    rotator.Transform.rotation = Quaternion.Euler(0, 0, turnAngle);
                }
            }
        }
    }
}