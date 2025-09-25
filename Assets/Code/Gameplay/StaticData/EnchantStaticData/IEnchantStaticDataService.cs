using Code.Gameplay.Enchants;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.EnchantStaticData
{
    public interface IEnchantStaticDataService
    {
        UniTask LoadAll();
        EnchantConfig GetEnchantConfig(EnchantTypeId typeId);
    }
}