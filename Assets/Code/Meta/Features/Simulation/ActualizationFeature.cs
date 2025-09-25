using Code.Common.Destruct;
using Code.Infrastructure.Systems;

namespace Code.Meta.Features.Simulation
{
  public sealed class ActualizationFeature : Feature
  {
    public ActualizationFeature(ISystemFactory systems)
    {
        Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}