using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;
using UnityEngine;

namespace Code.Meta.Skins.Configs
{
    [System.Serializable]
    public class SkinShopItem: IShopItem
    {
        [SerializeField] private ItemTypeId itemTypeId;
        public ItemTypeId ItemTypeId
        {
            get => itemTypeId;
            set => itemTypeId = value;
        }
        [SerializeField] private ItemStatusId itemStatusId;
        public ItemStatusId ItemStatusId
        {
            get => itemStatusId;
            set => itemStatusId = value;
        }
        [SerializeField] private int price;
        public int Price
        {
            get => price;
            set => price = value;
        }
        
        public short TypeId { get; set; }
    }
}