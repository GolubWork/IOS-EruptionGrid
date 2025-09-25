using System.Collections.Generic;
using Code.Gameplay.Cards.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.StaticData.cardsStaticData
{
    public class CardSpriteStaticDataProvider : ICardSpriteStaticDataProvider
    {
        private CardConfigs _configs;
        private Dictionary<CardTypeId, Sprite> _cards = new Dictionary<CardTypeId, Sprite>();
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
            await LoadConfigs();
        }
        
        public CardConfigs GetConfig() => _configs;
        
        public Sprite GetCardSpriteByID(CardTypeId typeId)
        {
            if (_cards.TryGetValue(typeId, out Sprite sprite))
                return sprite;
            return null;
        }
        
        
        private async UniTask LoadConfigs()
        {
            _configs = 
                await _assetProvider.LoadScriptable<CardConfigs>(ConfigsDirectoryConstants.CardConfigs);
            foreach (CardConfig cardConfig in _configs.CardConfig)
            {
                _cards[cardConfig.CardTypeId] = cardConfig.Sprite;
            }
        }
    }
}