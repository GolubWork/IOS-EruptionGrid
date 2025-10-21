using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class SwitchPlayerDirectionBySwipeSystem : IExecuteSystem
    {
        private readonly INewInputService _inputService;
        private readonly IGroup<GameEntity> _players;

        public SwitchPlayerDirectionBySwipeSystem(GameContext context, INewInputService inputService)
        {
            _inputService = inputService;
            _players = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.SwipeMovement
            ));
        }

        public void Execute()
        {
            _inputService.UpdateSwipe();

            if (!_inputService.SwipeDetected())
                return;
            
            foreach (GameEntity player in _players)
                player.ReplaceDirection(_inputService.SwipeDirection());
        }
    }
}