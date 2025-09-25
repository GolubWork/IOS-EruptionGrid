using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class RotateRandomDirectionSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _rotators;

        public RotateRandomDirectionSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            _rotators = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Transform,
                GameMatcher.RotationRandomDirection,
                GameMatcher.RotationSpeed
            ));
        }

        public void Execute()
        {
            foreach (GameEntity rotator in _rotators)
            {
                float rotationSpeed = rotator.RotationSpeed;
                float turnAngle = rotationSpeed * _timeService.DeltaTime;
                rotator.Transform.rotation *= Quaternion.Euler(0, 0, turnAngle);
            }
        }
    }
}