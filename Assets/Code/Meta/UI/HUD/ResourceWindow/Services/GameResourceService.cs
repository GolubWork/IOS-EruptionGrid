namespace Code.Meta.UI.HUD.ResourceWindow.Services
{
    public class GameResourceService: IGameResourceService
    {
        private GameResourceController _gameResourceController;
        public void SetGameResourceController(GameResourceController controller) => 
            _gameResourceController = controller;
        
        public GameResourceController GetGameResourceController() => 
            _gameResourceController;
    }
}