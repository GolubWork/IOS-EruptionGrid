namespace Code.Meta.Shop.Configs.ShopItems
{
    public interface IShopItem
    {
        public ItemTypeId ItemTypeId { get; set; }
        public ItemStatusId ItemStatusId { get; set; }
        public int Price { get; set; }
        
        public short TypeId { get; set; }
    }
}