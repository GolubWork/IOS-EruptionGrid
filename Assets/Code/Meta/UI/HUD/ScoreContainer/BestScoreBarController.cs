using Code.Meta.UI.HUD.ScoreContainer.Services;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD.ScoreContainer
{
    public class BestScoreBarController : UpdatableWindow
    {
        [SerializeField] private TextMeshProUGUI ScoreText;
        private BestScoreBarModel _model;
        private IBestScoreBarService _bestScoreBarService;
        private bool isScoreSeted;
        private IUpdatableWindowService _staticWindowService;

        [Inject]
        private void Construct(IUpdatableWindowService staticWindowService, IBestScoreBarService bestScoreBarService)
        {
            Id = UpdatableWindowId.BestScoreWindow;
            _model = new BestScoreBarModel();
            _bestScoreBarService = bestScoreBarService;
            _staticWindowService = staticWindowService;
        }
        protected override void Initialize()
        {
            _bestScoreBarService.SetBestScoreBar(this);
        }
        public void SetScore(float value)
        {
            if(isScoreSeted) return;
            ScoreText.text = _model.SetScore(value);
            isScoreSeted = true;
        }

        protected override void Cleanup()
        {
            _bestScoreBarService.SetBestScoreBar(null);
            isScoreSeted = false;
            _staticWindowService.Close(Id);
        }
    }
}