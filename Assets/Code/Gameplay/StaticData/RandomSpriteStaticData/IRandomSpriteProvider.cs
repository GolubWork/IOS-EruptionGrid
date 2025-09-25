using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.StaticData.RandomSpriteStaticData
{
    public interface IRandomSpriteProvider
    {
        UniTask LoadAll();
        Sprite GetRandomSprite();
    }
}