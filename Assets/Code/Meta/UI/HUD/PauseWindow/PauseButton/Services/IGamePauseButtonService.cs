namespace Code.Meta.UI.HUD.PauseWindow.PauseButton.Services
{
    public interface IGamePauseButtonService
    {
        GamePauseButtonWindow SetGamePauseButton(GamePauseButtonWindow gamePauseButtonWindow);
        GamePauseButtonWindow GetGamePauseButton();
    }
}