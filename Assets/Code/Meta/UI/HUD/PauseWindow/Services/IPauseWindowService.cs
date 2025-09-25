namespace Code.Meta.UI.HUD.PauseWindow.Services
{
    public interface IPauseWindowService
    {
        PauseWindowController SetGamePauseWindowController(PauseWindowController pauseWindowController);
        PauseWindowController GetPauseWindowController();
    }
}