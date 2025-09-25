using Code.Gameplay.LevelTimer.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.LevelTimer
{
    public class LevelTimerFeature: Feature
    {
        public LevelTimerFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeTimerSystem>());
            Add(systems.Create<DecreaseLevelTimerSystem>());
            Add(systems.Create<SetLevelTimeToUISystem>());
        }
    }
}