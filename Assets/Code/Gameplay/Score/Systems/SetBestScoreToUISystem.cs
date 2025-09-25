using Code.Meta.UI.HUD.ScoreContainer;
using Code.Meta.UI.HUD.ScoreContainer.Services;
using Entitas;

namespace Code.Gameplay.Score.Systems
{
    public class SetBestScoreToUISystem : IExecuteSystem
    {
        private readonly IBestScoreBarService _bestScoreBarService;
        private readonly IGroup<MetaEntity> _scoreStorages;

        public SetBestScoreToUISystem(MetaContext meta, IBestScoreBarService bestScoreBarService)
        {
            _bestScoreBarService = bestScoreBarService;
            _scoreStorages = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.BestScore
                ));
        }

        public void Execute()
        {
            foreach (MetaEntity storage in _scoreStorages)
            {
                BestScoreBarController bestScoreBar = _bestScoreBarService.GetBestScoreBar();
                if (bestScoreBar == null) continue;
                bestScoreBar.SetScore(storage.BestScore);
            }
        }
    }

}