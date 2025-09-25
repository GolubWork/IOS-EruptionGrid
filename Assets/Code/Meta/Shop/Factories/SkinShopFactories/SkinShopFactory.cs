using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Configs.ShopItems;
using Code.Meta.Skins.Configs;

namespace Code.Meta.Shop.Factories.SkinShopFactories
{
    public class SkinShopFactory : ISkinShopFactory
    {
        private readonly ISkinDataProvider _skinDataProvider;
        
        public SkinShopFactory(ISkinDataProvider skinDataProvider)
        {
            _skinDataProvider = skinDataProvider;
        }

        public void CreateSkinShop(ShopData shopData)
        {
            SkinConfig skinConfig = _skinDataProvider.GetSkinConfig();
            if (!IsSkinDataUptoDate(skinConfig)) 
                CreateNewSkinShopData(shopData, skinConfig);
        }

        public MetaEntity ChgangeSkinRequest(IShopItem shopItem)
        {
            return CreateMetaEntity.Empty()
                .AddRequestSkinTypeId((SkinTypeId)Enum.ToObject(typeof(SkinTypeId), shopItem.TypeId))
                .With(e => e.isChangeSkinRequest = true)
                ;
        }

        private bool IsSkinDataUptoDate(SkinConfig skinConfig)
        {
            MetaEntity skinShopContainer = Contexts.sharedInstance.meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.ShopStorage,
                MetaMatcher.Storage)).GetSingleEntity();
            if (skinShopContainer == null) return false;
            if (skinShopContainer.ShopStorage.Items.Count != skinConfig.SkinDatas.Count) return false;
            return true;
        }
        private MetaEntity CreateNewSkinShopData(ShopData shopData, SkinConfig skinConfig)
        {
            foreach (SkinData skinData in skinConfig.SkinDatas)
            {
                shopData.Items.Add(
                    new SkinShopItem()
                    {
                        ItemTypeId = skinData.shopItem.ItemTypeId,
                        ItemStatusId = skinData.shopItem.ItemStatusId,
                        Price = skinData.shopItem.Price,
                        TypeId = (short)skinData.skinTypeId,
                    });
            }
            
            return CreateMetaEntity.Empty()
                    .AddShopStorage(shopData)
                    .With(e => e.isStorage = true)
                    .With(e => e.isSkinShopData = true)
                ;
        }
    }
}