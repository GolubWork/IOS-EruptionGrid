using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class GrabFollowMouseXSystem : IExecuteSystem
    {
        private readonly INewInputService _inputService1;
        private readonly IGroup<GameEntity> _grabed;


        public GrabFollowMouseXSystem(GameContext game,
            INewInputService inputService1)
        {
            _inputService1 = inputService1;
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
                Vector2 mousePose = _inputService1.GetWorldTouchPosition();
                grabed.ReplaceWorldPosition(new Vector3(mousePose.x, grabed.WorldPosition.y, -1));
            }
        }
    }
}