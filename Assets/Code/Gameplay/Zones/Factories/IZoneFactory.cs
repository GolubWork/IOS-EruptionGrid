using Code.Gameplay.Zones.Configs;

namespace Code.Gameplay.Zones.Factories
{
    public interface IZoneFactory
    {
        GameEntity CreateZone(ZoneTypeId zoneTypeId);
    }
}