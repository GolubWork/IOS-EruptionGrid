using Code.Gameplay.Statuses.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Statuses
{
  public sealed class StatusFeature : Feature
  {
    public StatusFeature(ISystemFactory systems)
    {
      Add(systems.Create<StatusDurationSystem>());
      Add(systems.Create<PeriodicDamageStatusSystem>());
      Add(systems.Create<ApplyFreezeStatusSystem>());
      Add(systems.Create<ApplyExplosiveStatusSystem>());
      
      Add(systems.Create<CleanupUnappliedStatusLinkedChanges>());
      Add(systems.Create<CleanupUnappliedStatuses>());
    }
  }
}