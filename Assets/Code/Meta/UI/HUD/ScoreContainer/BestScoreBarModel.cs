using Code.Common.Helpers;

namespace Code.Meta.UI.HUD.ScoreContainer
{
    public class BestScoreBarModel
    {
        private const string BestScore = "BEST SCORE: ";
        public string SetScore(float value)
        {
            string scoreText = BestScore + value.ToString("");
            return StringUpdater.UpdateString(scoreText);
        }
    }
}