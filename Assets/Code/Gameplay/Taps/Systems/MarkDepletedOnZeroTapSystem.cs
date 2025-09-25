using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Taps.Systems
{
    public class MarkDepletedOnZeroTapSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _taped;
        private List<GameEntity> _buffer = new (1);

        public MarkDepletedOnZeroTapSystem(GameContext game)
        {
            _taped = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Taped,
                GameMatcher.TapsRequired
            ));
        }

        public void Execute()
        {
            foreach (GameEntity taped in _taped.GetEntities(_buffer))
            {
                if (taped.TapsRequired == 0)
                    taped.isTapDepleted = true;
                else
                    taped.isTaped = false;
            }
        }
    }
}