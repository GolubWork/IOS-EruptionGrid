using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.AdditionalSpriteProvider
{
    public interface IAdditionalSpriteProvider
    {
        UniTask LoadAll();
        AdditionalSpriteConfig GetConfig();
    }
}