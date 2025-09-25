using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.LifeTime.Systems
{
    public class MarkDeadOnMaxHealthSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new (16);

        public MarkDeadOnMaxHealthSystem(GameContext contextParameter)
        {
            _entities = contextParameter.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.CurrentHP,
                    GameMatcher.MaxHP)
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                if (entity.CurrentHP >= entity.MaxHP)
                {
                    entity.isDead = true;
                    entity.isProcessingDeath = true;
                }
            }
        }
    }
}