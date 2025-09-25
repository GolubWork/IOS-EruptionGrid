using Code.Gameplay.Items.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.ItemStaticData
{
    public interface IItemStaticDataService
    {
        UniTask LoadAll();
        ItemConfigs GetConfig();
        ItemConfig GetConfigById(ProducingItemTypeId typeId);
    }
}