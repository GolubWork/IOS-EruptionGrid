using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Common.Helpers;
using Code.Infrastructure.Identifiers;
using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;
using Code.Meta.Shop.Factories.SkinShopFactories;

namespace Code.Meta.Shop.Factories
{
    public class ShopFactory : IShopFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly ISkinShopFactory _skinShopFactory;

        public ShopFactory(
            IIdentifierService identifierService,
            ISkinShopFactory skinShopFactory)
        {
            _identifierService = identifierService;
            _skinShopFactory = skinShopFactory;
        }

        public MetaEntity BuyRequest(IShopItem shopItem)
           => CreateMetaEntity.Empty()
                .AddId(_identifierService.Next())
                .With(e => e.isShopRequest = true)
                .AddShopItem(shopItem)
                ;
        public MetaEntity SelectRequest(IShopItem shopItem)
        {
            switch (shopItem.ItemTypeId)
            {
                case ItemTypeId.Unknown:
                {
                    CustomDebug.LogWarning("Not implemented shop item type");
                    return null;
                }
                case ItemTypeId.Skin:
                {
                    return _skinShopFactory.ChgangeSkinRequest(shopItem);   
                }
            }
            return null;
        }
        

        public void CreateSkinShop(ShopData shopData) => _skinShopFactory.CreateSkinShop(shopData);
        
    }
}