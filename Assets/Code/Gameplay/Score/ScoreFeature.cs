using Code.Gameplay.Score.Systems;
using Code.Infrastructure.Systems;
using Code.Meta.UI;

namespace Code.Gameplay.Score
{
    public class ScoreFeature : Feature
    {
        public ScoreFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetBestScoreSystem>());
            Add(systems.Create<SetCurrentScoreToUISystem>());
            Add(systems.Create<SetBestScoreToUISystem>());
            Add(systems.Create<CleanUpCurrentScoreStorageSystem>());
        }
    }
}