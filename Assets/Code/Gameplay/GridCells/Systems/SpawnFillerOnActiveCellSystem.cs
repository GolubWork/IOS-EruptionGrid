using System;
using System.Collections.Generic;
using Code.Gameplay.GridCells.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grids.Systems
{
    public class SpawnFillerOnActiveCellSystem: IExecuteSystem
    {
        private readonly ICellFactory _cellFactory;
        private readonly IGroup<GameEntity> _cells;
        private List<GameEntity> _buffer = new (1);
        
        public SpawnFillerOnActiveCellSystem(
            GameContext game, 
            ICellFactory cellFactory)
        {
            _cellFactory = cellFactory;

            _cells = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GridCell,
                GameMatcher.ActiveCell
            ).NoneOf(GameMatcher.ActiveCellProcessed));
        }

        public void Execute()
        {
            foreach (GameEntity cell in _cells.GetEntities(_buffer))
            {
                _cellFactory.CreateCellFill(new Vector3(cell.WorldPosition.x, cell.WorldPosition.y, -1), cell.Id);
                cell.isActiveCellProcessed = true;
            }
        }
    }
}