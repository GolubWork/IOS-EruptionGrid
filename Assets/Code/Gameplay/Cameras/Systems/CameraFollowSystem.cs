using Entitas;
using UnityEngine;

namespace Code.Gameplay.Cameras.Systems
{
    public class CameraFollowSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cameras;

        public CameraFollowSystem(GameContext context)
        {
            _cameras = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Camera,
                GameMatcher.WorldPosition,
                GameMatcher.CameraFollowTargetId
                ));
        }

        public void Execute()
        {
            foreach (GameEntity camera in _cameras)
            {
                GameEntity targetEntity = Contexts.sharedInstance.game.GetEntityWithId(camera.CameraFollowTargetId);
                if(targetEntity == null) continue;
                Vector3 target = targetEntity.WorldPosition;
                target.z = camera.WorldPosition.z;
                target.x = 0;
                if(camera.WorldPosition.y > target.y) continue;
                camera.ReplaceWorldPosition(Vector3.Lerp(camera.WorldPosition, target, 0.1f));
            }
        }
    }
}