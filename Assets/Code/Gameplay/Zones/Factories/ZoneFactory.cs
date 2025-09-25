using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Zones.Configs;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Zones.Factories
{
    public class ZoneFactory : IZoneFactory
    {
        private readonly IIdentifierService _identifierService;

        public ZoneFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateZone(ZoneTypeId zoneTypeId)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                .With(e => e.isZone = true)
                .AddZoneTypeId(zoneTypeId)
                .AddZonePercent(20)
                .AddWorldPosition(Vector3.zero)
                .AddZoneWidth(0)
                .AddZoneHeight(0)
                .AddViewPath(PrefabsDirectoryConstants.ZonePrefabPath)
                ;
        }
        
    }
}