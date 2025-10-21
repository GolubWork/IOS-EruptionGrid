using Code.Gameplay.GameOver.Systems;
using Code.Gameplay.LifeTime.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.LifeTime
{
    public class DeathFeature: Feature
    {
        public DeathFeature(ISystemFactory systems)
        {
           // Add(systems.Create<UpdateHealthBarSystem>());
            
            Add(systems.Create<MarkDeadOnZeroHealthSystem>());
            Add(systems.Create<GameOverOnHeroDeathSystem>());
            
        }
    }
}