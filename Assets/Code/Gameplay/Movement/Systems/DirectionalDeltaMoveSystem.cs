using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class DirectionalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _movers;

        public DirectionalDeltaMoveSystem(GameContext gameContext, ITimeService time)
        {
            _time = time;
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Direction,
                    GameMatcher.Speed,
                    GameMatcher.MovementAvailable,
                    GameMatcher.Moving,
                    GameMatcher.MoveWithNoBounds)
            );
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                Move(mover);
            }
        }

        private void Move(GameEntity mover)
        {
            mover.ReplaceWorldPosition(NewWorldPosition(mover));
        }

        private Vector2 NewWorldPosition(GameEntity mover)
        {
            return (Vector2)mover.WorldPosition +
                   mover.Direction * mover.Speed * _time.DeltaTime;
        }
    }    
}