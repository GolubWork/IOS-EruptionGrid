using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class GrabFollowMouseXSystem : IExecuteSystem
    {
        private readonly ITouchInputService _inputService;
        private readonly IGroup<GameEntity> _grabed;


        public GrabFollowMouseXSystem(GameContext game,
            ITouchInputService inputService)
        {
            _inputService = inputService;
            _grabed = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grabed,
                GameMatcher.WorldPosition,
                GameMatcher.FollowMouseX
            ));
        }

        public void Execute()
        {
            foreach (GameEntity grabed in _grabed)
            {
                Vector2 mousePose = _inputService.GetWorldMousePosition();
                grabed.ReplaceWorldPosition(new Vector3(mousePose.x, grabed.WorldPosition.y, -1));
            }
        }
    }
}