using System.Collections.Generic;
using Code.Gameplay.Armaments.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Statuses.Systems
{
    public class ApplyExplosiveStatusSystem: IExecuteSystem
    {
        private readonly IArmamentFactory _armamentFactory;
        private readonly IGroup<GameEntity> _statuses;
        private readonly List<GameEntity> _buffer = new(32);

        public ApplyExplosiveStatusSystem(GameContext game, IArmamentFactory armamentFactory)
        {
            _armamentFactory = armamentFactory;
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Status,
                    GameMatcher.Explosive
                    )
                .NoneOf(GameMatcher.Affected));
        }

        public void Execute()
        {
            foreach (GameEntity status in _statuses.GetEntities(_buffer))
            {
                Debug.Log(status.WorldPosition);
                status.isAffected = true;
            }
        }
    }
}
