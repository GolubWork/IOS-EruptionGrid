using Code.Gameplay.Cards.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.StaticData.cardsStaticData
{
    public interface ICardSpriteStaticDataProvider
    {
        UniTask LoadAll();
        CardConfigs GetConfig();
        Sprite GetCardSpriteByID(CardTypeId typeId);
    }
}