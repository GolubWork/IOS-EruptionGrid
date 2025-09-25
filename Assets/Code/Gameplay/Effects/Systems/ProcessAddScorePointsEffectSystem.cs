using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Effects.Systems
{
    public class ProcessAddScorePointsEffectSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private readonly IGroup<MetaEntity> _scorePointsStorages;
        private List<GameEntity> _buffer = new (1);

        public ProcessAddScorePointsEffectSystem(GameContext game, MetaContext meta)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.AddPointsEffect,
                GameMatcher.EffectValue,
                GameMatcher.TargetId
            ).NoneOf(GameMatcher.Processed));

            _scorePointsStorages = meta.GetGroup(MetaMatcher.ScoreStorage);
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            foreach (MetaEntity storage in _scorePointsStorages)
            {
                storage.ReplaceSessionScore(storage.SessionScore + effect.EffectValue);
                effect.isProcessed = true;
            }
        }
    }
}