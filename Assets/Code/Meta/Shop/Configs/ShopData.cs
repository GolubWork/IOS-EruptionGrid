using System.Collections.Generic;
using Code.Meta.Shop.Configs.ShopItems;

namespace Code.Meta.Shop.Configs
{
    [System.Serializable]
    public class ShopData
    {
        public ShopTypeId ShopTypeId;
        public List<IShopItem> Items = new List<IShopItem>();
    }
}