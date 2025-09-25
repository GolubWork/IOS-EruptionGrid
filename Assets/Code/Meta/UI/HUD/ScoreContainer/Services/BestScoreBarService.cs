namespace Code.Meta.UI.HUD.ScoreContainer.Services
{
    public class BestScoreBarService : IBestScoreBarService
    {
        private BestScoreBarController _bestBestScoreBarController;
        public BestScoreBarController SetBestScoreBar(BestScoreBarController bestBestScoreBarController)
        {
            _bestBestScoreBarController = bestBestScoreBarController;
            return _bestBestScoreBarController;
        }
        public BestScoreBarController GetBestScoreBar()
        {
            return _bestBestScoreBarController;
        }    
    }
}
