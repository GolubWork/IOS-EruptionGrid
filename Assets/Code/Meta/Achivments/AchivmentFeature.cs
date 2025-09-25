using Code.Infrastructure.Systems;
using Code.Meta.Achivments.Systems;

namespace Code.Meta.Achivments
{
    public class AchivmentFeature: Feature
    {
        public AchivmentFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeAchivmentSystem>());
            Add(systems.Create<ProcessCurrencyCounterSystem>());
            Add(systems.Create<ProcessTapCounterSystem>());
        }
    }
}