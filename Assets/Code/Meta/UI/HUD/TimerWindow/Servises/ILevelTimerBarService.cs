namespace Code.Meta.UI.HUD.TimerWindow.Servises
{
    public interface ILevelTimerBarService
    {
        TimerWindowController SetTimerWindowController(TimerWindowController currentScoreBarController);
        TimerWindowController GetTimerWindowController();
    }
}