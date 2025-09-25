using System.Collections.Generic;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Meta.Shop.Configs.ShopItems;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;

namespace Code.Meta.UI.HUD.ShopWindow.SkinShop
{
    public class SkinShopWindowModel: ShopWindowModel
    {
        private IShopItem _currentItem;
        private List<IShopItem> _items;
        
        public SkinShopWindowModel(
            IStaticWindowService staticWindowService, 
            IUpdatableWindowService updatableWindowService, 
            IAudioFactory audioFactory, 
            IItemBar itemBar) : base(staticWindowService, updatableWindowService, audioFactory, itemBar)
        {
        }
        
        public void InitShop()
        {
            MetaEntity selectedSkin = Contexts.sharedInstance.meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.SelectedSkinStorage,
                MetaMatcher.Storage
            )).GetSingleEntity();
            
            _items = Contexts.sharedInstance.meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SkinShopData,
                MetaMatcher.ShopStorage
            )).GetSingleEntity().ShopStorage.Items;
            
            _currentItem = _items.Find(item => item.TypeId == (short)selectedSkin.SelectedSkinStorage);
            
            ItemBar.UpdateBar(_currentItem);
        }
        public void OnButtonleft(out (bool isFirst, bool isLast) isFirstOrLast)
        {
            AudioFactory.CreateSound(SoundTypeId.BtnClick);
            ItemBar.UpdateBar(GetPreviusItem());
            isFirstOrLast = IsFirstOrLast();
        }
        
        public void OnButtonRight(out (bool isFirst, bool isLast) isFirstOrLast)
        {
            AudioFactory.CreateSound(SoundTypeId.BtnClick);
            ItemBar.UpdateBar(GetNextItem());
            isFirstOrLast = IsFirstOrLast();
        }
        public (bool isFirst, bool isLast) IsFirstOrLast()
        {
            return (
                isFirst: CurrentIndex() == 0,
                isLast: CurrentIndex() == _items.Count - 1
            );
        }
        
        private IShopItem GetNextItem()
        {
            if (!IsFirstOrLast().isLast)
                _currentItem = _items[CurrentIndex() + 1];
            return _currentItem;
        }
        
        private IShopItem GetPreviusItem()
        {
            if (!IsFirstOrLast().isFirst)
                _currentItem = _items[CurrentIndex() - 1];
            return _currentItem;
        }
        
        private int CurrentIndex() => _items.FindIndex(item => item.TypeId == (short)_currentItem.TypeId);

    }
}