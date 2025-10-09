using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class GrabFollowMouseYSystem : IExecuteSystem
    {
        private readonly INewInputService _inputService1;
        private readonly IGroup<GameEntity> _grabed;


        public GrabFollowMouseYSystem(GameContext game,
            INewInputService inputService1)
        {
            _inputService1 = inputService1;
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
                Vector2 mousePose = _inputService1.GetWorldTouchPosition();
                grabed.ReplaceWorldPosition(new Vector3(grabed.WorldPosition.x, mousePose.y, -1));
            }
        }
    }
}