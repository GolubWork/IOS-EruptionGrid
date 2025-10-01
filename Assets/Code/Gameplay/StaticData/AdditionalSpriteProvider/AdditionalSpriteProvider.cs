using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.DependencyInjection;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.AdditionalSpriteProvider
{
    public class AdditionalSpriteProvider : IAdditionalSpriteProvider
    {
        private AdditionalSpriteConfig _sprites;
        private IAssetProvider _assetProvider;
        
        [Inject]
        private void Construct(
            IAssetProvider assetProvider
        )
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask LoadAll()
        {
            await LoadSprites();
        }
        
        public AdditionalSpriteConfig GetConfig() => _sprites;
        
        
        private async UniTask LoadSprites()
        {
            _sprites = 
                await _assetProvider.LoadScriptable<AdditionalSpriteConfig>(ConfigsDirectoryConstants.AdditionalSpriteConfig);
        }
    }
}