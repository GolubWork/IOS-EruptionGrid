using System.Collections.Generic;
using Code.Gameplay.StaticData.ShopStaticData;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;
using Code.Meta.UI.HUD.ShopWindow.Providers;
using Code.Meta.UI.HUD.ShopWindow.SkinShop;
using Entitas;

namespace Code.Meta.Shop.Systems
{
    public class ProcessBuyRequestSystem: IExecuteSystem
    {
        private readonly IShopProvider<SkinShopWindowController> _shopProvider;
        private readonly IGroup<MetaEntity> _buyRequests;
        private readonly IGroup<MetaEntity> _currencyStorages;
        private List<MetaEntity> _buffer = new (1);
        private readonly IGroup<MetaEntity> _shopData;

        public ProcessBuyRequestSystem(MetaContext meta, IShopProvider<SkinShopWindowController> shopProvider)
        {
            _shopProvider = shopProvider;

            _buyRequests = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.ShopRequest,
                MetaMatcher.ShopItem
            ).NoneOf(MetaMatcher.Processed));

            _currencyStorages = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.CurrencyStorage
            ));

            _shopData = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.ShopStorage,
                MetaMatcher.Storage,
                MetaMatcher.SkinShopData
            ));
        }

        public void Execute()
        {
            foreach (MetaEntity request in _buyRequests.GetEntities(_buffer))
            foreach (MetaEntity storage in _currencyStorages)
            {
                if (storage.CurrencyStorage <= request.ShopItem.Price)
                {
                    request.isProcessed = true;
                    return;
                }
                
                List<IShopItem> shopItems = _shopData.GetSingleEntity().ShopStorage.Items;
                ShopData shopData = _shopData.GetSingleEntity().ShopStorage;
                IShopItem shopItem =  shopItems.Find(_shopData=> _shopData.TypeId == request.ShopItem.TypeId);
                shopItem.ItemStatusId = ItemStatusId.Available;
                shopData.Items = shopItems;
                _shopData.GetSingleEntity().ReplaceShopStorage(shopData);
                
                storage.ReplaceCurrencyStorage(storage.CurrencyStorage - request.ShopItem.Price);
                _shopProvider.GetController().UpdateBar();
                request.isProcessed = true;
            }
        }
    }
}