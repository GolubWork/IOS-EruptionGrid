using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Items.Configs;
using Code.Gameplay.StaticData.ItemStaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Items.Factories
{
    public class ItemFactory : IItemFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IItemStaticDataService _itemStaticDataService;

        public ItemFactory(IIdentifierService identifiers, IItemStaticDataService itemStaticDataService)
        {
            _identifiers = identifiers;
            _itemStaticDataService = itemStaticDataService;
        }
        
        public GameEntity CreateItem(Vector3 at, ProducingItemTypeId itemTypeId, int StartPoint, int EndPoint)
        {
            ItemConfig config = _itemStaticDataService.GetConfigById(itemTypeId);
            return CreateGameEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddViewPrefab(config.prefab)
                    .AddItemTypeId(config.typeId) 
                    .AddStartPoint(StartPoint)
                    .AddEndPoint(EndPoint)
                    .AddDistance(0)
                    .AddCurrentLerp(0)
                    .With(e => e.isConveyourBeltItem = true)
                    .With(e => e.isItem = true)
                ;
        }
    }
}