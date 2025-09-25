using System.Collections;
using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.GridCells.Factories;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Infrastructure;
using Entitas;
using UnityEngine;

public class SpawnMirrorCellsOnGridSystem : IExecuteSystem
    {
        private readonly ICellFactory _gridFactory;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGroup<GameEntity> _playerGrids;
        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _cameras;

        private const float gap = 1f;
        

        public SpawnMirrorCellsOnGridSystem(
            GameContext game,
            ICellFactory gridFactory,
            IAdditionalSpriteProvider additionalSpriteProvider,
            ICoroutineRunner coroutineRunner)
        {
            _gridFactory = gridFactory;
            _additionalSpriteProvider = additionalSpriteProvider;
            _coroutineRunner = coroutineRunner;

            _playerGrids = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grid,
                GameMatcher.PlayerGrid,
                GameMatcher.GridRows
            ).NoneOf(GameMatcher.Processed));

            _cameras = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Camera,
                GameMatcher.MainCamera
            ));
        }


public void Execute()
{
    foreach (GameEntity playerGrid in _playerGrids.GetEntities(_buffer))
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
            cam.transform.position.y - (rows * cellHeight) / 2 + cellHeight / 2 + gap,
            0f
        );

        for (int x = 0; x < cols; x++)
        for (int y = 0; y < rows; y++)
        {
            float posX = origin.x + x * cellWidth;
            float posY = origin.y + y * cellHeight;

            GameEntity bottomCell = _gridFactory.CreateMirrorGridCell(new Vector3(posX, posY, 0f));

            bottomCell
                .AddLinkedGridId(playerGrid.Id)
                .AddCellGridCoordinates(new Vector2Int(x, y))
                .With(e => e.isActiveCell = playerGrid.GridRows.columns[x].rows[y]);

            _coroutineRunner.StartCoroutine(SetSpriteSize(bottomCell, cellWidth, cellHeight));
        }

        playerGrid.isProcessed = true;
    }
}

private IEnumerator SetSpriteSize(GameEntity bottomCell, float cellWidth, float cellHeight)
{
    while (!bottomCell.hasSpriteRenderer)
        yield return null;

    SpriteRenderer sr = bottomCell.SpriteRenderer;

    while (!bottomCell.hasTransform)
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
    

