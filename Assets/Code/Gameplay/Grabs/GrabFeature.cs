using Code.Gameplay.Grabs.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Grabs
{
    public class GrabFeature: Feature
    {
        public GrabFeature(ISystemFactory systems)
        {
            Add(systems.Create<GrabSystem>());
            Add(systems.Create<GrabFollowMouseXYSystem>());
            Add(systems.Create<GrabFollowMouseYSystem>());
            Add(systems.Create<GrabFollowMouseXSystem>());
            Add(systems.Create<DropSystem>());
        }
    }
}