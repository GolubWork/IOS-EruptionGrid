using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Chicken.Systems
{
    public class FinalizeChickenDeathProcessSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly List<GameEntity> _buffer = new(1);

        public FinalizeChickenDeathProcessSystem(GameContext contextParameter)
        {
            _heroes = contextParameter.GetGroup(GameMatcher.AllOf(
                GameMatcher.Chicken,
                GameMatcher.Dead,
                GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes.GetEntities(_buffer))
            {
                hero.isProcessingDeath = false;
            }
        }
    }
}