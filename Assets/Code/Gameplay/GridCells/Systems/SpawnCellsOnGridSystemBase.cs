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
    public abstract class SpawnCellsOnGridSystemBase : IExecuteSystem
    {
        protected readonly ICellFactory _gridFactory;
        protected readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        protected readonly ICoroutineRunner _coroutineRunner;
        protected readonly IGroup<GameEntity> _cameras;
        protected readonly List<GameEntity> _buffer = new(1);

        protected const float VerticalGap = 0.5f;
        protected const float VerticalMarginFraction = 0.25f;

        protected SpawnCellsOnGridSystemBase(
            GameContext game,
            ICellFactory gridFactory,
            IAdditionalSpriteProvider additionalSpriteProvider,
            ICoroutineRunner coroutineRunner)
        {
            _gridFactory = gridFactory;
            _additionalSpriteProvider = additionalSpriteProvider;
            _coroutineRunner = coroutineRunner;

            _cameras = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Camera,
                GameMatcher.MainCamera
            ));
        }

        protected abstract IGroup<GameEntity> TargetGroup { get; }
        protected abstract float GetGridYOffset(Camera cam, float halfGridHeight, float totalHeight);

        public void Execute()
        {
            foreach (GameEntity grid in TargetGroup.GetEntities(_buffer))
            {
                if (grid.isProcessed)
                    continue;

                foreach (GameEntity cameraEntity in _cameras)
                {
                    Camera cam = cameraEntity.Camera;
                    if (cam.orthographicSize < 10f)
                    {
                        Debug.LogError($"[SpawnCells] Camera orthographicSize is too small: {cam.orthographicSize}. Expected at least 10. Skipping grid spawn.");
                        continue;
                    }
                    int cols = grid.GridRows.X;
                    int rows = grid.GridRows.Y;

                    float worldHeight = 2f * cam.orthographicSize;
                    float worldWidth = worldHeight * cam.aspect;

                    float availableHeight = worldHeight * (1f - 2f * VerticalMarginFraction) - VerticalGap;
                    float gridHeight = availableHeight / 2f;

                    float cellSizeByHeight = gridHeight / rows;
                    float cellSizeByWidth = worldWidth / cols;
                    float cellSize = Mathf.Min(cellSizeByHeight, cellSizeByWidth);


                    float totalGridHeight = rows * cellSize;
                    float totalGridWidth = cols * cellSize;

                    Vector3 center = cam.transform.position + new Vector3(0, GetGridYOffset(cam, totalGridHeight / 2f, availableHeight), 0);

                    Vector3 origin = new Vector3(
                        center.x - totalGridWidth / 2f + cellSize / 2f,
                        center.y - totalGridHeight / 2f + cellSize / 2f,
                        0f
                    );

                    Sprite gridCellSprite = _additionalSpriteProvider.GetConfig().GridCell;
                    float spriteSize = gridCellSprite.bounds.size.x;

                    for (int x = 0; x < cols; x++)
                    {
                        for (int y = 0; y < rows; y++)
                        {
                            Vector3 pos = new Vector3(
                                origin.x + x * cellSize,
                                origin.y + y * cellSize,
                                0f
                            );

                            GameEntity cell = CreateCell(grid, pos, x, y);
                            _coroutineRunner.StartCoroutine(SetSpriteScale(cell, spriteSize, cellSize));
                        }
                    }

                    grid.isProcessed = true;
                }
            }
        }

        protected abstract GameEntity CreateCell(GameEntity grid, Vector3 pos, int x, int y);

        private IEnumerator SetSpriteScale(GameEntity cell, float originalSpriteSize, float targetCellSize)
        {
            while (!cell.hasSpriteRenderer)
                yield return null;

            while (!cell.hasTransform)
                yield return null;

            Transform transform = cell.Transform;
            
            transform.localScale = Vector3.one;
            
            yield return null;
            
            float scale = targetCellSize / originalSpriteSize;
            transform.localScale = new Vector3(scale, scale, 1f);
            
        }
    }
}
