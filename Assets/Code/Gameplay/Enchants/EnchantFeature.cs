using Code.Gameplay.Enchants.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Enchants
{
  public sealed class EnchantFeature : Feature
  {
    public EnchantFeature(ISystemFactory systems)
    {
      Add(systems.Create<ExplosiveEnchantSystem>());
    }
  }
}