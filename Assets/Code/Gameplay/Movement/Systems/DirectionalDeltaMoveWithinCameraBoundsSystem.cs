using Code.Gameplay.Common.AABB;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class DirectionalDeltaMoveWithinCameraBoundsSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IAABBPhysicsService _aabbPhysicsService;
        private readonly IGroup<GameEntity> _movers;
        private readonly IGroup<GameEntity> _borderCameras;

        public DirectionalDeltaMoveWithinCameraBoundsSystem(GameContext gameContext, ITimeService time,  IAABBPhysicsService aabbPhysicsService)
        {
            _time = time;
            _aabbPhysicsService = aabbPhysicsService;
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Direction,
                    GameMatcher.Speed,
                    GameMatcher.MovementAvailable,
                    GameMatcher.Moving,
                    GameMatcher.MoveInCameraBounds)
            );

            _borderCameras = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.BorderCamera
            
            ));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            foreach (GameEntity borderCamera in _borderCameras)
            {
                Move(mover, borderCamera);
            }
        }

        private void Move(GameEntity mover, GameEntity borderCamera)
        {
            mover.ReplaceWorldPosition(NewWorldPosition(mover, borderCamera));
        }

        private Vector2 NewWorldPosition(GameEntity mover, GameEntity borderCamera)
        {
            Vector2 newWorldPosition = mover.WorldPosition;
        
            Vector2 spriteSize = mover.SpriteRenderer.bounds.size;
                
            ((bool xWithinBounds, float xOutDistance), (bool yWithinBounds, float yOutDistance))  xyWithinCameraBounds =
                _aabbPhysicsService.XYWithinCameraBounds(newWorldPosition, spriteSize, borderCamera);
        
            if (!xyWithinCameraBounds.Item1.xWithinBounds || !xyWithinCameraBounds.Item2.yWithinBounds)
            {
                if (!xyWithinCameraBounds.Item1.xWithinBounds)
                {
                    newWorldPosition.x -= xyWithinCameraBounds.Item1.xOutDistance;
                }
                if (!xyWithinCameraBounds.Item2.yWithinBounds)
                {
                    newWorldPosition.y -= xyWithinCameraBounds.Item2.yOutDistance;
                }
            }
        
            return newWorldPosition +
                   mover.Direction * mover.Speed * _time.DeltaTime;
        }
    }
}