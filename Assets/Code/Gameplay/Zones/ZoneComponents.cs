using Code.Gameplay.Zones.Configs;
using Entitas;

namespace Code.Gameplay.Zones
{
    [Game] public class Zone : IComponent { }
    [Game] public class ZoneTypeIdComponent : IComponent { public ZoneTypeId Value; }
    [Game] public class ZonePercent : IComponent { public float Value; }
    [Game] public class ZoneWidth : IComponent { public float Value; }
    [Game] public class ZoneHeight : IComponent { public float Value; }
    [Game] public class ZonePlaced : IComponent { }
    
    
}