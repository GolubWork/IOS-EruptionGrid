using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.TargetCollection.Systems
{
    public class MarkReachedSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new (128);

        public MarkReachedSystem(GameContext contextParameter)
        {
            _entities = contextParameter.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetsBuffer)
                .NoneOf(GameMatcher.Reached));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                if (entity.TargetsBuffer.Count > 0)
                    entity.isReached = true;
            }
        }
    }

}