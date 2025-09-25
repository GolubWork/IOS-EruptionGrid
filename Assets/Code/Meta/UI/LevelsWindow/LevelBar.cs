using System;
using Code.Meta.Levels.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.LevelsWindow
{
    public class LevelBar: MonoBehaviour
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private TextMeshProUGUI _levelText;
       
        private LevelData _levelData;
        public event Action<LevelData> OnLevelSelected;

        public void InitBar(LevelData levelData)
        {
            _levelData = levelData;
            _levelText.text = ((int)levelData.levelId).ToString();
            ActiveText(levelData.levelStatusId);
            _levelButton.onClick.AddListener(OnLevelClick);
        }

        private void OnDestroy()
        {
            _levelButton.onClick.RemoveListener(OnLevelClick);
        }

        private void ActiveText(LevelStatusId levelStatusId)
        {
            switch (levelStatusId)
            {
                case LevelStatusId.Closed:
                {
                    _levelText.color = Color.gray;
                    break;
                }
                case LevelStatusId.Opened:
                {
                    _levelText.color = Color.white;
                    break;
                }
                case LevelStatusId.Current:
                {
                    _levelText.color = Color.white;
                    break;
                }
                case LevelStatusId.Finished:
                {
                    _levelText.color = Color.white;
                    break;
                }
            }
        }
        
        private void OnLevelClick()
        {
            if(_levelData.levelStatusId == LevelStatusId.Closed) return;
            OnLevelSelected?.Invoke(_levelData);
        }
    }
}