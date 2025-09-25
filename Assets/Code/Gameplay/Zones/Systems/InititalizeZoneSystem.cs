using Code.Gameplay.Zones.Configs;
using Code.Gameplay.Zones.Factories;
using Entitas;

namespace Code.Gameplay.Zones.Systems
{
    public class InititalizeZoneSystem: IInitializeSystem
    {
        private readonly IZoneFactory _zoneFactory;

        public InititalizeZoneSystem(IZoneFactory zoneFactory)
        {
            _zoneFactory = zoneFactory;
        }

        public void Initialize()
        {
            _zoneFactory.CreateZone(ZoneTypeId.InCameraViewTop);
        }

    }
}