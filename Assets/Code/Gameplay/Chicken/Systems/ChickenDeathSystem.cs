using Entitas;

namespace Code.Gameplay.Chicken.Systems
{
    public class ChickenDeathSystem : IExecuteSystem
    {
        private const float DeathAnimationTime = 2;
        private readonly IGroup<GameEntity> _heroes;

        public ChickenDeathSystem(GameContext contextParameter)
        {
            _heroes = contextParameter.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Chicken,
                GameMatcher.Dead,
                GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                hero.isMovementAvailable = false;
                hero.isTurnedAlongDirection = false;
                
                // TODO Remove Collections here
                
                hero.ReplaceSelfDestructTimer(DeathAnimationTime);
            }
        }
    }
}