using Code.Gameplay.Common.AABB;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Cameras.Systems
{
    public class CameraBoundsCheckSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IAABBPhysicsService _aabbPhysicsService;
        private readonly IGroup<GameEntity> _movers;
        private readonly IGroup<GameEntity> _cameras;

        public CameraBoundsCheckSystem(GameContext gameContext, ITimeService timeService,
            IAABBPhysicsService aabbPhysicsService)
        {
            _timeService = timeService;
            _aabbPhysicsService = aabbPhysicsService;
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Chicken,
                    GameMatcher.WorldPosition,
                    GameMatcher.Direction,
                    GameMatcher.Speed,
                    GameMatcher.SpriteRenderer
                ));

            _cameras = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Camera,
                    GameMatcher.BorderCamera,
                    GameMatcher.WorldPosition)
            );
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            foreach (GameEntity camera in _cameras)
            {
                Vector2 newWorldPosition = NewWorldPosition(mover);
                Vector2 spriteSize = mover.SpriteRenderer.bounds.size;
                
                ((bool xWithinBounds, float xOutDistance), (bool yWithinBounds, float yOutDistance))  xyWithinCameraBounds =
                    _aabbPhysicsService.XYWithinCameraBounds(newWorldPosition, spriteSize, camera);
                Vector2 newDirection = Vector2.zero;

                if (!xyWithinCameraBounds.Item1.xWithinBounds || !xyWithinCameraBounds.Item2.yWithinBounds)
                {
                    if (!xyWithinCameraBounds.Item1.xWithinBounds)
                    {
                        newDirection = new Vector2(-xyWithinCameraBounds.Item1.xOutDistance, mover.Direction.y);
                    }
                    if (!xyWithinCameraBounds.Item2.yWithinBounds)
                    {
                        newDirection = new Vector2(mover.Direction.x, -xyWithinCameraBounds.Item2.yOutDistance);
                    }
                    
                    mover.ReplaceDirection(newDirection);
                }
            }
        }

        private Vector2 NewWorldPosition(GameEntity mover)
        {
            return (Vector2)mover.WorldPosition + mover.Direction * mover.Speed * _timeService.DeltaTime;
        }
    }
}