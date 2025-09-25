using System.Collections.Generic;
using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;
using Entitas;

namespace Code.Meta.Skins.Systems
{
    // TODO Replace to SHOP Feature
    public class ProcessSkinUnlockSystem: IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _requests;
        private readonly IGroup<MetaEntity> _shopDatas;
        private List<MetaEntity> _buffer = new (1);

        public ProcessSkinUnlockSystem(MetaContext meta)
        {
            _requests = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Id,
                MetaMatcher.RequestSkinTypeId
            ).NoneOf(MetaMatcher.Processed));
            
            _shopDatas = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.ShopStorage,
                MetaMatcher.Storage
            ));
        }

        public void Execute()
        {
            foreach (MetaEntity request in _requests.GetEntities(_buffer))
            foreach (MetaEntity shopData in _shopDatas)
            {
               IShopItem shopItem = shopData.ShopStorage.Items.Find(data => data.TypeId == (short)request.RequestSkinTypeId);
               shopItem.ItemStatusId = ItemStatusId.Purchasable;
               request.isProcessed = true;
            }
        }
    }
}