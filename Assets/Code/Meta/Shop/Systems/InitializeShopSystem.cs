using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData.ShopStaticData;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Meta.Shop.Configs;
using Code.Meta.Shop.Factories;
using Entitas;

namespace Code.Meta.Shop.Systems
{
    public class InitializeShopSystem: IInitializeSystem
    {
        private readonly IShopDataProvider _shopDataProvider;
        private readonly ISkinDataProvider _skinDataProvider;
        private readonly IShopFactory _shopFactory;
        private readonly IGroup<MetaEntity> _shopData;

        public InitializeShopSystem(MetaContext meta,
            IShopDataProvider shopDataProvider,
            ISkinDataProvider skinDataProvider,
            IShopFactory shopFactory
            )
        {
            _shopDataProvider = shopDataProvider;
            _skinDataProvider = skinDataProvider;
            _shopFactory = shopFactory;

            _shopData = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.ShopStorage
            ));
        }

        public void Initialize()
        {
            CreateNewShopData();
        }

        private void CreateNewShopData()
        {
            ShopConfig config = _shopDataProvider.GetShopConfig();
            foreach (ShopData shopData in config.ShopDatas)
            {
                switch (shopData.ShopTypeId)
                {
                    case ShopTypeId.Unknown:
                    {
                        break;
                    }
                    case ShopTypeId.SkinShop:
                    {
                        _shopFactory.CreateSkinShop(shopData);
                        break;
                    }
                }
            }
        }
    }
}