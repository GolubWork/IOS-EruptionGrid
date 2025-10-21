using Code.Common.Extensions;
using Code.Gameplay.GridCells.Factories;
using Code.Gameplay.Grids.Systems;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Infrastructure;
using Entitas;
using UnityEngine;

public class SpawnMirrorCellsOnGridSystem : SpawnCellsOnGridSystemBase
{
    private readonly IGroup<GameEntity> _playerGrids;

    public SpawnMirrorCellsOnGridSystem(
        GameContext game,
        ICellFactory factory,
        IAdditionalSpriteProvider sprites,
        ICoroutineRunner runner)
        : base(game, factory, sprites, runner)
    {
        _playerGrids = game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Grid,
            GameMatcher.PlayerGrid,
            GameMatcher.GridRows
        ).NoneOf(GameMatcher.Processed));
    }

    protected override IGroup<GameEntity> TargetGroup => _playerGrids;

    protected override float GetGridYOffset(Camera cam, float halfGridHeight, float totalHeight)
    {
        float worldHeight = 2f * cam.orthographicSize;
        float topLimit = worldHeight / 2f - worldHeight * VerticalMarginFraction;
        // центр верхней сетки
        return topLimit - halfGridHeight;
    }

    protected override GameEntity CreateCell(GameEntity grid, Vector3 pos, int x, int y)
    {
        var cell = _gridFactory.CreateMirrorGridCell(pos);

        cell
            .AddLinkedGridId(grid.Id)
            .AddCellGridCoordinates(new Vector2Int(x, y))
            .With(e => e.isActiveCell = grid.GridRows.columns[x].rows[y]);

        return cell;
    }
}