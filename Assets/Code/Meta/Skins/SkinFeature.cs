using Code.Infrastructure.Systems;
using Code.Meta.Skins.Systems;

namespace Code.Meta.Skins
{
    public class SkinFeature: Feature
    {
        public SkinFeature(ISystemFactory systems)
        {
            Add(systems.Create<ApplySkinSystem>());
            Add(systems.Create<ProcessSkinUnlockSystem>());
            Add(systems.Create<ProcessChangeSelectedSkinRequestSystem>());
        }
    }
}