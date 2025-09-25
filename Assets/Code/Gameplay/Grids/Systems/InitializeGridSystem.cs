using System;
using _Scripts.GridSpawn;
using Code.Gameplay.Grids.Factories;
using Code.Meta.Levels.Configs;
using Entitas;

namespace Code.Gameplay.Grids.Systems
{
    public class InitializeGridSystem: IInitializeSystem
    {
        private readonly IGridFactory _gridFactory;
        private readonly IGroup<MetaEntity> _levels;

        public InitializeGridSystem(MetaContext meta, IGridFactory gridFactory)
        {
            _gridFactory = gridFactory;
            _levels = meta.GetGroup(MetaMatcher.ChosenLevel);
        }
        
        public void Initialize()
        {
            foreach (MetaEntity level in _levels)
            {
                GridRows gridRows = level.ChosenLevel.levelStatusId == LevelStatusId.Infinity ? 
                    CreateRandomGrid() : level.ChosenLevel.grid;
                _gridFactory.CreateReferenceGridRequest(gridRows);
            }
        }

        private GridRows CreateRandomGrid()
        {
            var random = new Random();
            var grid = new GridRows { X = 5, Y = 5 };
            grid.InitializeGrid();
            int trueCount = 0;
            for (int i = 0; i < grid.X; i++)
            {
                for (int j = 0; j < grid.Y; j++)
                {
                    bool value = random.NextDouble() > 0.5;
                    grid.columns[i].rows[j] = value;
                    if (value) trueCount++;
                }
            }
            while (trueCount < 4)
            {
                int x = random.Next(0, grid.X);
                int y = random.Next(0, grid.Y);

                if (!grid.columns[x].rows[y])
                {
                    grid.columns[x].rows[y] = true;
                    trueCount++;
                }
            }
            return grid;
        }


    }
}