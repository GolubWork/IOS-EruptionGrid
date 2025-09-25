using Code.Gameplay.Taps.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Taps
{
    public class TapFeature: Feature
    {
        public TapFeature(ISystemFactory systems)
        {
            Add(systems.Create<MarkTapedOnTapSystem>());
            Add(systems.Create<CreateVisualEffectOnTapSystem>());
            Add(systems.Create<MarkDepletedOnZeroTapSystem>());
            Add(systems.Create<CreateEffectOnDepletedSystem>());
            Add(systems.Create<ProcessDepletedTaps>());
        }
    }
}