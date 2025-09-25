using System.Collections.Generic;
using Code.Gameplay.GridCells.Factories;
using Entitas;

namespace Code.Gameplay.Grids.Systems
{
    public class RemoveFillerOnNonActiveCellSystem: IExecuteSystem
    {
        private readonly ICellFactory _cellFactory;
        private readonly IGroup<GameEntity> _cells;
        private List<GameEntity> _buffer = new (1);
        private List<GameEntity> _buffer2 = new (1);
        private readonly IGroup<GameEntity> _fillers;

        public RemoveFillerOnNonActiveCellSystem(
            GameContext game, 
            ICellFactory cellFactory)
        {
            _cellFactory = cellFactory;

            _cells = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GridCell,
                GameMatcher.ActiveCellProcessed
            ).NoneOf(GameMatcher.ActiveCell));
            
            _fillers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.CellFiller,
                GameMatcher.LinkedCellId
            ));
        }

        public void Execute()
        {
            foreach (GameEntity cell in _cells.GetEntities(_buffer))
            foreach (GameEntity filler in _fillers.GetEntities(_buffer2))
            {
                if(cell.Id != filler.LinkedCellId) continue;
                _cellFactory.ReturnCellFill(filler);
                cell.isActiveCellProcessed = false;
            }
        }
    }
}