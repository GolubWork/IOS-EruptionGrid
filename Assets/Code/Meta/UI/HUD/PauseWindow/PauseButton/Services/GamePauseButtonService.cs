namespace Code.Meta.UI.HUD.PauseWindow.PauseButton.Services
{
    public class GamePauseButtonService : IGamePauseButtonService
    {
        private GamePauseButtonWindow _gamePauseButtonWindow;
        
        public GamePauseButtonWindow SetGamePauseButton(GamePauseButtonWindow gamePauseButtonWindow)
        {
            _gamePauseButtonWindow = gamePauseButtonWindow;
            return _gamePauseButtonWindow;
        }
        public GamePauseButtonWindow GetGamePauseButton()
        {
            return _gamePauseButtonWindow;
        }  
    }
}