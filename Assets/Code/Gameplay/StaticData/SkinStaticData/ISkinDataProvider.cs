using Code.Meta.Skins.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.SkinStaticData
{
    public interface ISkinDataProvider
    {
        UniTask LoadAll();
        SkinData GetSkinDataById(SkinTypeId skinTypeId);
        SkinConfig GetSkinConfig();
    }
}