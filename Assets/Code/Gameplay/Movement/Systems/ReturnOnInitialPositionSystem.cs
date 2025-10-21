using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class ReturnOnInitialPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _returners;

        public ReturnOnInitialPositionSystem(GameContext context)
        {
            _returners = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.InitialeWorldPosition,
                GameMatcher.WorldPosition
            ));
        }

        public void Execute()
        {
            foreach (GameEntity returner in _returners)
            {
                // if (returner.Direction.x > 0 && returner.WorldPosition.x > 12)
                //     returner.ReplaceWorldPosition(returner.InitialeWorldPosition);
                // if (returner.Direction.x < 0 && returner.WorldPosition.x < -12)
                //     returner.ReplaceWorldPosition(returner.InitialeWorldPosition);

                if ((returner.Direction.x > 0 && returner.WorldPosition.x > 12) || (returner.Direction.x < 0 && returner.WorldPosition.x < -12) )
                {
                    bool overlap = false;
                    foreach (var other in _returners)
                    {
                        if (other == returner) continue;
                        if (Mathf.Abs(other.WorldPosition.x - returner.InitialeWorldPosition.x) < 5f &&
                            Mathf.Abs(other.WorldPosition.y - returner.WorldPosition.y) < 0.1f)
                        {
                            overlap = true;
                            break;
                        }
                    }

                    if (!overlap)
                        returner.ReplaceWorldPosition(returner.InitialeWorldPosition);
                }
            }
        }
    }
}