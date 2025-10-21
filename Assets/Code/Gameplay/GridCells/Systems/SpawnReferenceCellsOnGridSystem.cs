using Code.Common.Extensions;
using Code.Gameplay.GridCells.Factories;
using Code.Gameplay.Grids.Systems;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Infrastructure;
using Entitas;
using UnityEngine;

public class SpawnReferenceCellsOnGridSystem : SpawnCellsOnGridSystemBase
{
    private readonly IGroup<GameEntity> _referenceGrids;

    public SpawnReferenceCellsOnGridSystem(
        GameContext game,
        ICellFactory factory,
        IAdditionalSpriteProvider sprites,
        ICoroutineRunner runner)
        : base(game, factory, sprites, runner)
    {
        _referenceGrids = game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Grid,
            GameMatcher.ReferenceGrid,
            GameMatcher.GridRows
        ).NoneOf(GameMatcher.Processed));
    }

    protected override IGroup<GameEntity> TargetGroup => _referenceGrids;

    protected override float GetGridYOffset(Camera cam, float halfGridHeight, float totalHeight)
    {
        float worldHeight = 2f * cam.orthographicSize;
        float bottomLimit = -worldHeight / 2f + worldHeight * VerticalMarginFraction;
        // центр нижней сетки
        return bottomLimit + halfGridHeight;
    }

    protected override GameEntity CreateCell(GameEntity grid, Vector3 pos, int x, int y)
    {
        int invertedY = grid.GridRows.Y - 1 - y;
        var cell = _gridFactory.CreateGridCell(pos);

        cell
            .AddLinkedGridId(grid.Id)
            .AddCellGridCoordinates(new Vector2Int(x, invertedY))
            .With(e => e.isActiveCell = grid.GridRows.columns[x].rows[invertedY]);

        return cell;
    }
}