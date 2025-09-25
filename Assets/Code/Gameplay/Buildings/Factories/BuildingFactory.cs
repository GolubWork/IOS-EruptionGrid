using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Buildings.Configs;
using Code.Gameplay.StaticData.BuildingStaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Buildings.Factories
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IBuildingStaticDataProvider _buildingStaticDataProvider;

        public BuildingFactory(IIdentifierService identifiers, IBuildingStaticDataProvider buildingStaticDataProvider)
        {
            _identifiers = identifiers;
            _buildingStaticDataProvider = buildingStaticDataProvider;
        }

        public GameEntity Create(Vector3 at, BuildingTypeId buildingTypeId)
        {
            BuildingConfig config = _buildingStaticDataProvider.GetConfigById(buildingTypeId);
            
            return CreateGameEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddBuildingTypeId(buildingTypeId)
                    .AddRecievingItemsTypeId(config.RecivingItems)
                    .AddProducingItemsTypeId(config.ProducingItems)
                    .AddProducingSpeed(config.ProducingSpeed)
                    .AddMaxBuildingStorage(config.MaxStorage)
                    .AddCurBuildingStorage(config.CurStorage)
                    .AddViewPrefab(config.ViewPrefab)
                    .AddWorldPosition(at)
                    .With(e => e.isBuilding = true)
                ;
        }
    }
}