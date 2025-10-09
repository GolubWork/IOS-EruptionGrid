using System.Collections.Generic;
using Code.Gameplay.Cameras;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class DropSystem: IExecuteSystem
    {
        private readonly INewInputService _inputService1;
        private readonly IGroup<GameEntity> _grabed;
        private List<GameEntity> _buffer = new (1);


        public DropSystem(GameContext game,
            INewInputService inputService1)
        {
            _inputService1 = inputService1;
            _grabed = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grabed
            ));
        }

        public void Execute()
        {
            if (_inputService1.GetLeftMouseButtonUp())
            {
                foreach (GameEntity grabed in _grabed.GetEntities(_buffer))
                {
                    grabed.isGrabed = false;
                }
            }
        }
    }
}