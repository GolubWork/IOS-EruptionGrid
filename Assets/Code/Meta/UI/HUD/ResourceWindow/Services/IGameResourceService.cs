namespace Code.Meta.UI.HUD.ResourceWindow.Services
{
    public interface IGameResourceService
    {
        void SetGameResourceController(GameResourceController controller);
        GameResourceController GetGameResourceController();
        
    }
}