using Code.Gameplay.Buildings.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.BuildingStaticData
{
    public interface IBuildingStaticDataProvider
    {
        UniTask LoadAll();
        BuildingConfigs GetConfig();
        BuildingConfig GetConfigById(BuildingTypeId typeId);
    }
}