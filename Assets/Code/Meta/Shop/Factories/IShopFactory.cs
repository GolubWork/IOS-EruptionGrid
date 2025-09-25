using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;

namespace Code.Meta.Shop.Factories
{
    public interface IShopFactory
    {
        MetaEntity BuyRequest(IShopItem shopItem);
        MetaEntity SelectRequest(IShopItem shopItem);
        void CreateSkinShop(ShopData shopData);
    }
}