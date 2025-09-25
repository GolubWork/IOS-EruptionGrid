using Code.Gameplay.Zones.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Zones
{
    public class ZoneFeature: Feature
    {
        public ZoneFeature(ISystemFactory systems)
        {
            Add(systems.Create<InititalizeZoneSystem>());
            Add(systems.Create<PlaceZoneSystem>());
            Add(systems.Create<AddZoneTapEffectSystem>());
        }
    }
}