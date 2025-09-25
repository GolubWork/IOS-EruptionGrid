using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Effects.Systems
{
    public class CleanUpProcessedEffects: ICleanupSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanUpProcessedEffects(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Effect,
                GameMatcher.Processed
            ));
        }

        public void Cleanup()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            {
                effect.Destroy();
            }
        }
    }
}