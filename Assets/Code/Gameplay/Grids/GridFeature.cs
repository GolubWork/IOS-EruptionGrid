using Code.Gameplay.Grids.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Grids
{
    public class GridFeature: Feature
    {
        public GridFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeGridSystem>());
            Add(systems.Create<ProcessGridBuildRequestSystem>());
            Add(systems.Create<InitializeMirrorGridSystem>());
            Add(systems.Create<ProcessGridMirrorBuildRequestSystem>());
        }
    }
}