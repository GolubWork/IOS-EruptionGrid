using Code.Infrastructure.EntityViews.Systems.AudioEntityViewSystems;
using Code.Infrastructure.EntityViews.Systems.GameEntityViewSystems;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.EntityViews
{
    public sealed class BindViewFeature : Feature
    {
        public BindViewFeature(ISystemFactory systems)
        {
            Add(systems.Create<BindGameEntityViewFromPathSystem>());
            Add(systems.Create<BindGameEntityViewFromPrefabSystem>());

            Add(systems.Create<BindAudioEntityViewFromPathSystem>());
            Add(systems.Create<BindAudioEntityViewFromPrefabSystem>());
        }
    }
}