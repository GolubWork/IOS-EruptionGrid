using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;
using Code.Progress;
using Entitas;

namespace Code.Meta.Shop
{
    [Meta] public class ShopStorage : ISavedComponent { public ShopData Value; }
    [Meta] public class SkinShopData : ISavedComponent {  }
    
    
    [Meta] public class ShopRequest : IComponent { }
    [Meta] public class ShopItem : IComponent { public IShopItem Value; }
    
    
    
    
}