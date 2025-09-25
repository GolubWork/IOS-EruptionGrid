using Code.Common.Helpers;
using TMPro;
using UnityEngine;

namespace Code.Meta.UI.HUD.LeaderboardWindow
{
    public class LeaderBoardBar: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpScore;
        [SerializeField] private TextMeshProUGUI tmpName;
        [SerializeField] private TextMeshProUGUI tmpPosition;

        public void Setup(string name, float score, int position)
        {
            tmpScore.text = StringUpdater.UpdateString(score.ToString());
            tmpName.text = StringUpdater.UpdateString(name);
            tmpPosition.text = StringUpdater.UpdateString(position.ToString());
        }

        public void Highlight()
        {
            tmpScore.color = Color.yellow;
            tmpName.color = Color.yellow;
            tmpPosition.color = Color.yellow;
        }
    }
}