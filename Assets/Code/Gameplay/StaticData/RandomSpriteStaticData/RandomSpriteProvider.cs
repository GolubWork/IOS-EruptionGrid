using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.StaticData.RandomSpriteStaticData
{
    public class RandomSpriteProvider : IRandomSpriteProvider
    {
        private SpriteConfig _sprites;
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
        
        public Sprite GetRandomSprite()
        {
            Sprite randomSprite = _sprites.sprites[UnityEngine.Random.Range(0, _sprites.sprites.Count)];
            return randomSprite;
        }
        
        private async UniTask LoadSprites()
        {
            _sprites = 
                await _assetProvider.LoadScriptable<SpriteConfig>(ConfigsDirectoryConstants.RandomSpriteConfig);
        }
    }
}