using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Floors.Factories
{
    public class FloorFactory : IFloorFactory
    {
        private readonly IIdentifierService _identifierService;

        public FloorFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateFloor(Vector3 at)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddViewPath(PrefabsDirectoryConstants.FloorPrefabPath)
                ;
        }
    }
}