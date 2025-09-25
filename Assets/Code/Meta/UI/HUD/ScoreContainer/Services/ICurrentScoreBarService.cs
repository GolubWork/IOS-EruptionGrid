namespace Code.Meta.UI.HUD.ScoreContainer.Services
{
    public interface ICurrentScoreBarService
    {
        CurrentScoreBarController SetCurrentScoreBar(CurrentScoreBarController currentScoreBarController);
        CurrentScoreBarController GetCurrentScoreBar();
    }
}