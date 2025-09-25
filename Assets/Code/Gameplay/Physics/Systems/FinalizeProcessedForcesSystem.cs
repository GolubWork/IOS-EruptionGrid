using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Physics.Systems
{
    public class FinalizeProcessedForcesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _forces;
        private List<GameEntity> _buffer = new(12);
        private readonly IGroup<GameEntity> _forcesWithLifeTime;

        public FinalizeProcessedForcesSystem(GameContext contextParameter)
        {
            _forces = contextParameter.GetGroup(GameMatcher.AllOf(
                GameMatcher.ForceApplier,
                GameMatcher.Processed
            ));
        }

        public void Execute()
        {
            foreach (GameEntity force in _forces.GetEntities(_buffer))
            {
                force.isDestructed = true;
            }
        }
    }
}