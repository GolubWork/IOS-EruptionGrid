using System;
using _Scripts.GridSpawn;
using UnityEngine;

namespace Code.Meta.Levels.Configs
{
    [Serializable]
    public class LevelData
    {
        public LevelId levelId;
        public LevelStatusId levelStatusId;
        public float levelSeconds = 60;
        public int gameResource = 10;
        public GridRows grid = new GridRows(); 
        
        private void OnEnable() {
            if (grid.columns == null || grid.columns.Length == 0) {
                grid.InitializeGrid();
            }
        }
    }
}