using System;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Meta.Shop.Configs.ShopItems;
using Code.Meta.Shop.Factories;
using Code.Meta.Skins.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.ShopWindow.SkinShop
{
    public class SkinItemBar: MonoBehaviour, IItemBar
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private GameObject itemPriceContainer;
        [SerializeField] private TextMeshProUGUI itemPrice;
        [SerializeField] private Button btnSelect;
        [SerializeField] private TextMeshProUGUI btnText;
        
        private IShopFactory _shopFactory;
        private IShopItem _shopItem;
        private ISkinDataProvider _skinDataProvider;
        private IAudioFactory _audioFactory;

        public void Init(
            IShopFactory shopFactory, 
            ISkinDataProvider skinDataProvider,
            IAudioFactory audioFactory)
        {
            _shopFactory = shopFactory;
            _skinDataProvider = skinDataProvider;
            _audioFactory = audioFactory;
        }
        
        public void UpdateBar(IShopItem shopItem)
        {
            RemoveListener();
            _shopItem = shopItem;
            SkinData skinData = 
                _skinDataProvider.GetSkinDataById((SkinTypeId)Enum.ToObject(typeof(SkinTypeId), shopItem.TypeId));

                itemImage.sprite = skinData.Sprite;
                itemPrice.text = StringUpdater.UpdateString(shopItem.Price.ToString(""));
                MetaEntity selectedSkin = Contexts.sharedInstance.meta.GetGroup(MetaMatcher.AllOf(
                    MetaMatcher.Storage,
                    MetaMatcher.SelectedSkinStorage
                )).GetSingleEntity();
                bool isCurrentSkin = selectedSkin.SelectedSkinStorage == (SkinTypeId)Enum.ToObject(typeof(SkinTypeId), _shopItem.TypeId);
                
            btnText.text = StringUpdater.UpdateString(ButtonTextByItemStatus(shopItem.ItemStatusId, isCurrentSkin));
            AddListenersByItemStatus(shopItem.ItemStatusId);
        }

        public void UpdateBar()
        {
            RemoveListener();
            MetaEntity selectedSkin = Contexts.sharedInstance.meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SelectedSkinStorage
            )).GetSingleEntity();
            bool isCurrentSkin = selectedSkin.SelectedSkinStorage == (SkinTypeId)Enum.ToObject(typeof(SkinTypeId), _shopItem.TypeId);
                
            btnText.text = StringUpdater.UpdateString(ButtonTextByItemStatus(_shopItem.ItemStatusId, isCurrentSkin));
            AddListenersByItemStatus(_shopItem.ItemStatusId);
        }
        
        private string ButtonTextByItemStatus(ItemStatusId itemStatus, bool isCurrentSkin)
        {
            switch (itemStatus)
            {
                case ItemStatusId.Unknown:
                {
                    return "Unknow Item Status";
                }
                case ItemStatusId.Available:
                {
                    if (isCurrentSkin)
                    {
                        return "Selected";
                    }
                    else
                    {
                        return "Available";
                    }
                }
                case ItemStatusId.Purchasable:
                {
                    return "Purchase";
                }
                case ItemStatusId.Unavailable:
                {
                    return "Locked";
                }
            }
            return String.Empty;
        }

        private void AddListenersByItemStatus(ItemStatusId itemStatus)
        {
            switch (itemStatus)
            {
                case ItemStatusId.Unknown:
                {
                    btnSelect.onClick.AddListener(NotImplemented);
                    break;
                }
                case ItemStatusId.Available:
                {
                    itemPriceContainer.SetActive(false);
                    btnSelect.onClick.AddListener(Select);
                    break;
                }
                case ItemStatusId.Purchasable:
                {
                    itemPriceContainer.SetActive(true);
                    btnSelect.onClick.AddListener(Purchase);
                    break;
                }
                case ItemStatusId.Unavailable:
                {
                    itemPriceContainer.SetActive(false);
                    btnSelect.onClick.AddListener(NotImplemented);
                    break;
                }
            }
        }

        private void RemoveListener()
        {
            btnSelect.onClick.RemoveAllListeners();
        }
        private void NotImplemented()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            CustomDebug.LogWarning("Not Implemented");
        }

        private void Purchase()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _shopFactory.BuyRequest(_shopItem);
        }
        
        private void Select()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _shopFactory.SelectRequest(_shopItem);
        }
    }
}