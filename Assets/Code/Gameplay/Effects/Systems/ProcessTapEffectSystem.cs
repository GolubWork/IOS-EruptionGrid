using System.Collections.Generic;
using Code.Gameplay.Eggs.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Effects.Systems
{
    public class ProcessTapEffectSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private List<GameEntity> _buffer = new (1);
        private readonly IGroup<GameEntity> _gameResrouces;
        private readonly IGroup<MetaEntity> _score;

        public ProcessTapEffectSystem(GameContext game, MetaContext meta)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TapEffect,
                GameMatcher.Effect,
                GameMatcher.ProducerId,
                GameMatcher.TargetId
            ));

            _gameResrouces = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GameResource,
                GameMatcher.ResourceValue
            ));
            
            _score = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionScore
            ));
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            {
                GameEntity cell = Contexts.sharedInstance.game.GetEntityWithId(effect.ProducerId);
                cell.isActiveCell = !cell.isActiveCell;
                _gameResrouces.GetSingleEntity().ReplaceResourceValue( _gameResrouces.GetSingleEntity().ResourceValue - 1);
                float score = _gameResrouces.GetSingleEntity().ResourceValue * Random.Range(0.5f, 1.5f);
                if (score == 0)
                    _score.GetSingleEntity().ReplaceSessionScore(0);
                else
                    _score.GetSingleEntity().ReplaceSessionScore(_score.GetSingleEntity().SessionScore + score);
                effect.isProcessed = true;
            }
        }
    }
}