using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Effects.Systems
{
    public class ProcessDamageEffect: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private List<GameEntity> _buffer = new (1);
        private readonly IGroup<GameEntity> _heroes;

        public ProcessDamageEffect(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.DamageEffect
            ));
            
            _heroes = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Chicken,
                GameMatcher.CurrentHP,
                GameMatcher.MaxHP
            ));
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            foreach (GameEntity hero in _heroes)
            {
                hero.ReplaceCurrentHP(hero.CurrentHP - effect.EffectValue);
                effect.isProcessed = true;
            }
        }
    }
}