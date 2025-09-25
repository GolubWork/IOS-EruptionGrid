using System.Collections.Generic;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Meta.Shop.Configs;
using Code.Meta.UI.HUD.ShopWindow.Providers;
using Code.Meta.UI.HUD.ShopWindow.SkinShop;
using Entitas;

namespace Code.Meta.Skins.Systems
{
    public class ProcessChangeSelectedSkinRequestSystem: IExecuteSystem
    {
        private readonly IShopProvider<SkinShopWindowController> _shopProvider;
        private readonly IGroup<MetaEntity> _requests;
        private readonly IGroup<MetaEntity> _selectedSkins;
        private List<MetaEntity> _buffer = new (1);
        private readonly IGroup<MetaEntity> _shopData;

        public ProcessChangeSelectedSkinRequestSystem(MetaContext meta, IShopProvider<SkinShopWindowController> shopProvider)
        {
            _shopProvider = shopProvider;
            _requests = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.ChangeSkinRequest,
                MetaMatcher.RequestSkinTypeId
            ).NoneOf(MetaMatcher.Processed));
            
            _selectedSkins = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.SelectedSkinStorage,
                MetaMatcher.Storage
            ));
            
            _shopData = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.ShopStorage,
                MetaMatcher.Storage,
                MetaMatcher.SkinShopData
            ));
        }

        public void Execute()
        {
            foreach (MetaEntity request in _requests.GetEntities(_buffer))
            {
                _selectedSkins.GetSingleEntity().ReplaceSelectedSkinStorage(request.RequestSkinTypeId);
                _shopProvider.GetController().UpdateBar();
                request.isProcessed = true;
            }
        }
    }
}