using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Meta.Shop.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.ShopStaticData
{
    public class ShopDataProvider : IShopDataProvider
    {
        private readonly IAssetProvider _assetProvider;
        private ShopConfig _shopConfig;

        public ShopDataProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask LoadAll()
        {
            await LoadShop();
        }
        
        public ShopConfig GetShopConfig() => _shopConfig;
        
        private async UniTask LoadShop()
        {
            _shopConfig = await _assetProvider.LoadScriptable<ShopConfig>(ConfigsDirectoryConstants.ShopConfigPath);
        }
    }
}