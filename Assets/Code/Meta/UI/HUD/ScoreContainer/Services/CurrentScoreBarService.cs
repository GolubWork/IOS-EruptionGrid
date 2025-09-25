namespace Code.Meta.UI.HUD.ScoreContainer.Services
{
    public class CurrentScoreBarService : ICurrentScoreBarService
    {
        private CurrentScoreBarController _currentScoreBarController;
        public CurrentScoreBarController SetCurrentScoreBar(CurrentScoreBarController currentScoreBarController)
        {
            _currentScoreBarController = currentScoreBarController;
            return _currentScoreBarController;
        }
        public CurrentScoreBarController GetCurrentScoreBar()
        {
            return _currentScoreBarController;
        }   
    }
}