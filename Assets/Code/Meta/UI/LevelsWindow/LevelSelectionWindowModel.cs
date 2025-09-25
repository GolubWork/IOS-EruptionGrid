using System.Collections.Generic;
using Code.Meta.Levels.Configs;
using UnityEngine;

namespace Code.Meta.UI.HUD.LevelsWindow
{
    public class LevelSelectionWindowModel
    {
        private readonly MetaContext _metaContext;

        public LevelSelectionWindowModel(MetaContext metaContext)
        {
            _metaContext = metaContext;
        }

        public void CreatelevelBars(RectTransform levelBarContainer, LevelBar levelBarPrefab, List<LevelData> levelContainerLevels)
        {
            foreach (LevelData levelData in levelContainerLevels)
            {
                
            }
        }
    }
}