using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class GrabFollowMouseYSystem : IExecuteSystem
    {
        private readonly ITouchInputService _inputService;
        private readonly IGroup<GameEntity> _grabed;


        public GrabFollowMouseYSystem(GameContext game,
            ITouchInputService inputService)
        {
            _inputService = inputService;
            _grabed = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grabed,
                GameMatcher.WorldPosition,
                GameMatcher.FollowMouseY
            ));
        }

        public void Execute()
        {
            foreach (GameEntity grabed in _grabed)
            {
                Vector2 mousePose = _inputService.GetWorldMousePosition();
                grabed.ReplaceWorldPosition(new Vector3(grabed.WorldPosition.x, mousePose.y, -1));
            }
        }
    }
}