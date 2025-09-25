namespace Code.Meta.UI.HUD.TimerWindow.Servises
{
    public class LevelTimerBarService : ILevelTimerBarService
    {
        private TimerWindowController _CurrentTimerWindowController;
        public TimerWindowController SetTimerWindowController(TimerWindowController currentScoreBarController)
        {
            _CurrentTimerWindowController = currentScoreBarController;
            return _CurrentTimerWindowController;
        }
        public TimerWindowController GetTimerWindowController()
        {
            return _CurrentTimerWindowController;
        }   
    }
}