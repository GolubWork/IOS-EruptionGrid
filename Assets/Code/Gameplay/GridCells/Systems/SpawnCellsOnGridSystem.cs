using System.Collections.Generic;
using Code.Gameplay.GridCells.Factories;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grids.Systems
{
    public class SpawnCellsOnGridSystem: IExecuteSystem
    {
        private readonly ICellFactory _gridFactory;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly IGroup<GameEntity> _grids;
        private List<GameEntity> _buffer = new (1);
        private readonly IGroup<GameEntity> _cameras;
        
        public SpawnCellsOnGridSystem(GameContext game, ICellFactory gridFactory, IAdditionalSpriteProvider additionalSpriteProvider)
        {
            _gridFactory = gridFactory;
            _additionalSpriteProvider = additionalSpriteProvider;

            _grids = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grid,
                GameMatcher.ReferenceGrid,
                GameMatcher.GridRows
            ).NoneOf(GameMatcher.Processed));
            
            _cameras = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Camera,
                GameMatcher.MainCamera
                ));
        }

        public void Execute()
        {
            foreach (GameEntity grid in _grids.GetEntities(_buffer))
            foreach (GameEntity camera in _cameras)
            {
                Camera cam = camera.Camera;

                float cellWidth = _additionalSpriteProvider.GetConfig().GridCell.bounds.size.x;
                float cellHeight = _additionalSpriteProvider.GetConfig().GridCell.bounds.size.y;

                Vector3 screenCenter = cam.ScreenToWorldPoint(
                    new Vector3(Screen.width / 2f, Screen.height / 2f, cam.nearClipPlane)
                );

                float gridWidth = grid.GridRows.X * cellWidth;
                float gridHeight = grid.GridRows.Y * cellHeight;

                float startX = screenCenter.x - (gridWidth / 2.0f) + (cellWidth / 2.0f);
                float startY = screenCenter.y - (gridHeight / 2.0f) + (cellHeight / 2.0f);

                for (int x = 0; x < grid.GridRows.X; x++)
                for (int y = 0; y < grid.GridRows.Y; y++)
                {
                    float posX = startX + x * cellWidth;
                    float posY = startY + y * cellHeight;
                    GameEntity gridCell = _gridFactory.CreateGridCell(new Vector3(posX, posY, 0));
                    gridCell.isActiveCell = grid.GridRows.columns[x].rows[y];
                }
                grid.isProcessed = true;
            }
        }
    }
}