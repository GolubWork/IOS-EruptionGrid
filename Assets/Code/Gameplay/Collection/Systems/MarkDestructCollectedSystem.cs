using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Collection.Systems
{
    public class MarkDestructCollectedSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _collected;
        private List<GameEntity> _buffer = new (1);

        public MarkDestructCollectedSystem(GameContext game)
        {
            _collected = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Collected
            ));
        }

        public void Execute()
        {
            foreach (GameEntity collected in _collected.GetEntities(_buffer))
            {
                collected.isDestructed = true;
            }
        }
    }
}