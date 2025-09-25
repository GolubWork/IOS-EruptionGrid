using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.ConveyorBelt.Systems
{
    public class MarkReachedEndSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _items;
        private readonly List<GameEntity> _buffer = new(1);

        public MarkReachedEndSystem(GameContext game)
        {
            _items = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.CurrentLerp,
                GameMatcher.StartPoint,
                GameMatcher.LinkedBeltId
            ).NoneOf(GameMatcher.ReachedEnd));
        }

        public void Execute()
        {
            foreach (GameEntity item in _items.GetEntities(_buffer))
            {
                if (item.CurrentLerp >= 1)
                {
                    GameEntity belt = Contexts.sharedInstance.game.GetEntityWithId(item.LinkedBeltId);
                    if (item.StartPoint + 2 < belt.LineRenderer.positionCount)
                    {
                        item.ReplaceCurrentLerp(0);
                        item.ReplaceStartPoint(item.StartPoint + 1);
                    }
                    else
                    {
                        item.isReachedEnd = true;
                    }
                }
            }
        }

    }
}