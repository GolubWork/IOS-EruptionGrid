using Code.Infrastructure.Systems;
using Code.Meta.Shop.Systems;

namespace Code.Meta.Shop
{
    public class ShopFeature: Feature
    {
        public ShopFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeShopSystem>());
            Add(systems.Create<ProcessBuyRequestSystem>());
        }
    }
}