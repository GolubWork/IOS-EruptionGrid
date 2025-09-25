using Entitas;

namespace Code.Gameplay.Grids.Systems
{
    public class PlayerGridsCellMatcher: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _playerGrid;
        private readonly IGroup<GameEntity> _cells;

        public PlayerGridsCellMatcher(GameContext game)
        {
            _playerGrid = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.PlayerGrid,
                GameMatcher.GridRows
                ));

            _cells = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.LinkedGridId,
                GameMatcher.CellGridCoordinates
            ));
        }

        public void Execute()
        {
            foreach (GameEntity grid in _playerGrid)
            foreach (GameEntity cell in _cells)
            {
                grid.GridRows.columns[cell.CellGridCoordinates.x].rows[cell.CellGridCoordinates.y] = cell.isActiveCell;
            }
        }
    }
}