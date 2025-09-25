using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Effects.Systems
{
    public class ProcessAddCurrencyEffect: IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _storages;
        private readonly IGroup<GameEntity> _effects;
        private List<GameEntity> _buffer = new (1);

        public ProcessAddCurrencyEffect(GameContext game, MetaContext meta)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.AddCurrencyEffect
            ));

            _storages = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.GoldStorage,
                MetaMatcher.CurrentGold
            ));
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            foreach (MetaEntity storage in _storages)
            {
                storage.ReplaceCurrentGold(storage.CurrentGold + effect.EffectValue);
                effect.isProcessed = true;
            }
        }
    }
}