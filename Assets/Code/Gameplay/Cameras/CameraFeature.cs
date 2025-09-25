using Code.Gameplay.Cameras.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Cameras
{
    public class CameraFeature: Feature
    {
        public CameraFeature(ISystemFactory systems)
        {
            Add(systems.Create<CreatePhysicsBordersSystem>());
        }
    }
}