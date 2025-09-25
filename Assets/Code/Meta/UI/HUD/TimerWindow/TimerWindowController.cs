using Code.Common.Helpers;
using Code.Meta.UI.HUD.TimerWindow.Servises;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD.TimerWindow
{
    public class TimerWindowController : UpdatableWindow
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private const string defaultTimeText = "";

        private IUpdatableWindowService _updatableWindowService;
        private ILevelTimerBarService _levelTimerBarService;

        [Inject]
        private void Construct(
            IUpdatableWindowService updatableWindowService,
            ILevelTimerBarService levelTimerBarService
        )
        {
            Id = UpdatableWindowId.TimerWindow;
            _updatableWindowService = updatableWindowService;
            _levelTimerBarService = levelTimerBarService;
        }

        protected override void Initialize()
        {
            _levelTimerBarService.SetTimerWindowController(this);
        }

        public void UpdateTime(float seconds)
        {
            int minutes = (int)(seconds / 60);
            int remainingSeconds = (int)(seconds % 60);
            string timetext = $"{minutes:00}:{remainingSeconds:00}";
            timerText.text = StringUpdater.UpdateString($"{defaultTimeText} {timetext}");
        }
    }
}