using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;

namespace Code.Meta.Shop.Factories.SkinShopFactories
{
    public interface ISkinShopFactory
    {
        void CreateSkinShop(ShopData shopData);
        MetaEntity ChgangeSkinRequest(IShopItem shopItem);
    }
}