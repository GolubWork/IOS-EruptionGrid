using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class MoveChickenToScreenCenterSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _chickens;
        private readonly IGroup<GameEntity> _cameras;

        public MoveChickenToScreenCenterSystem(GameContext gameContext)
        {
            _chickens = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Chicken,
                    GameMatcher.MovableByInput,
                    GameMatcher.WorldPosition,
                    GameMatcher.Direction,
                    GameMatcher.Speed,
                    GameMatcher.MovementAvailable,
                    GameMatcher.Moving,
                    GameMatcher.Dead)
            );

            _cameras = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.MainCamera
            ));
        }

        
        public void Execute()
        {
            foreach (GameEntity chicken in _chickens)
            foreach (GameEntity camera in _cameras)
            {
                chicken.isMovableByInput = false;
                chicken.isMoveInCameraBounds = false;
                chicken.isMoveWithNoBounds = true;
                
                Vector3 cameraPosition = camera.worldPosition.Value;
                Vector3 screenCenter = cameraPosition;

                chicken.ReplaceDirection(screenCenter);
            }
        }
    }
}