using Code.Gameplay.Backgrounds.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Backgrounds
{
    public class BackgroundFeature: Feature
    {
        public BackgroundFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetCameraToCanvasSystem>());
        }
    }
}