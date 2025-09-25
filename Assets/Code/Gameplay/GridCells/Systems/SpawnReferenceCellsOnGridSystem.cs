using System.Collections;
using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.GridCells.Factories;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Infrastructure;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grids.Systems
{
    public class SpawnReferenceCellsOnGridSystem : IExecuteSystem
    {
        private readonly ICellFactory _gridFactory;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGroup<GameEntity> _referenceGrids;
        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _cameras;

        private const float gap = 0;

        public SpawnReferenceCellsOnGridSystem(
            GameContext game,
            ICellFactory gridFactory,
            IAdditionalSpriteProvider additionalSpriteProvider,
            ICoroutineRunner coroutineRunner)
        {
            _gridFactory = gridFactory;
            _additionalSpriteProvider = additionalSpriteProvider;
            _coroutineRunner = coroutineRunner;

            _referenceGrids = game.GetGroup(GameMatcher.AllOf(
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
            foreach (GameEntity playerGrid in _referenceGrids.GetEntities(_buffer))
            foreach (GameEntity camera in _cameras)
            {
                Camera cam = camera.Camera;

                int cols = playerGrid.GridRows.X;
                int rows = playerGrid.GridRows.Y;

                float worldScreenHeight = 2f * cam.orthographicSize;
                float worldScreenWidth = worldScreenHeight * cam.aspect;

                float originalCellWidth = _additionalSpriteProvider.GetConfig().GridCell.bounds.size.x;
                float originalCellHeight = _additionalSpriteProvider.GetConfig().GridCell.bounds.size.y;

                float maxCellWidth = worldScreenWidth / cols;
                float maxCellHeight = worldScreenHeight / rows;

                float cellWidth = Mathf.Min(originalCellWidth, maxCellWidth);
                float cellHeight = Mathf.Min(originalCellHeight, maxCellHeight);

                Vector3 origin = new Vector3(
                    cam.transform.position.x - (cols * cellWidth) / 2 + cellWidth / 2,
                    cam.transform.position.y - (rows * cellHeight) / 2 + cellHeight / 2 - gap,
                    0f
                );
                
                for (int x = 0; x < cols; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        int invertedY = rows - 1 - y; 

                        float posX = origin.x + x * cellWidth;
                        float posY = origin.y - y * cellHeight;

                        GameEntity cell = _gridFactory.CreateGridCell(new Vector3(posX, posY, 0f));

                        cell
                            .AddLinkedGridId(playerGrid.Id)
                            .AddCellGridCoordinates(new Vector2Int(x, invertedY))
                            .With(e => e.isActiveCell = playerGrid.GridRows.columns[x].rows[invertedY]);

                        _coroutineRunner.StartCoroutine(SetSpriteSize(cell, cellWidth, cellHeight));
                    }
                }

                playerGrid.isProcessed = true;
            }
        }

        private IEnumerator SetSpriteSize(GameEntity cell, float cellWidth, float cellHeight)
        {
            while (!cell.hasSpriteRenderer)
                yield return null;

            SpriteRenderer sr = cell.SpriteRenderer;

            while (!cell.hasTransform)
                yield return null;

            float originalWidth = sr.sprite.bounds.size.x;
            float originalHeight = sr.sprite.bounds.size.y;

            sr.transform.localScale = new Vector3(
                Mathf.Min(1f, cellWidth / originalWidth),
                Mathf.Min(1f, cellHeight / originalHeight),
                1f
            );
        }
    }
}
