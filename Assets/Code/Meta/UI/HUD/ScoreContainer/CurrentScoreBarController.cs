using Code.Meta.UI.HUD.ScoreContainer.Services;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD.ScoreContainer
{
    public class CurrentScoreBarController: UpdatableWindow
    {
        [SerializeField] private TextMeshProUGUI ScoreText;
        private CurrentScoreBarModel _model;
        private ICurrentScoreBarService _currentScoreBarService;
        private IUpdatableWindowService _staticWindowService;

        [Inject]
        private void Construct(
            IUpdatableWindowService staticWindowService, 
            ICurrentScoreBarService currentScoreBarService)
        {
            Id = UpdatableWindowId.CurrentScoreWindow;
            _model = new CurrentScoreBarModel();
            _currentScoreBarService = currentScoreBarService;
            _staticWindowService = staticWindowService;
        }
        protected override void Initialize()
        {
            _currentScoreBarService.SetCurrentScoreBar(this);
        }
        public void SetScore(float value)
        {
            ScoreText.text = _model.SetScore(value);
        }
        protected override void Cleanup()
        {
            _currentScoreBarService.SetCurrentScoreBar(null);
            _staticWindowService.Close(Id);
        }
    }
}