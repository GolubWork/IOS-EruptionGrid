using Code.Common.Helpers;

namespace Code.Meta.UI.HUD.ScoreContainer
{
    public class CurrentScoreBarModel
    {
        public string SetScore(float value)
        {
            string scoreText = value.ToString("");
            return StringUpdater.UpdateString(scoreText);
        }
    }
}