using Code.Meta.Shop.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.ShopStaticData
{
    public interface IShopDataProvider
    {
        UniTask LoadAll();
        ShopConfig GetShopConfig();
    }
}