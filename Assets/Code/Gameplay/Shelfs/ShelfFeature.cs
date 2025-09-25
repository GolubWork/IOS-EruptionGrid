using Code.Gameplay.Shelfs.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Shelfs
{
    public class ShelfFeature: Feature
    {
        public ShelfFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeShelfSystem>());
            Add(systems.Create<MarkOnShelfSystem>());
        }
    }
}