using _Scripts.GridSpawn;
using Code.Common.Helpers;
using Code.Gameplay.Grids.Factories;
using Entitas;

namespace Code.Gameplay.Grids.Systems
{
    public class InitializeMirrorGridSystem:  IExecuteSystem
    {
        private readonly IGridFactory _gridFactory;
        private readonly IGroup<GameEntity> _referencedGrids;
        private bool inited;

        public InitializeMirrorGridSystem(
            GameContext game,
            IGridFactory gridFactory)
        {
            _gridFactory = gridFactory;
            _referencedGrids = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Grid,
                GameMatcher.GridRows,
                GameMatcher.ReferenceGrid
            ));
        }
        
        public void Execute()
        {
            if(inited) return;
            foreach (GameEntity grid in _referencedGrids)
            {
                GridRows reference = grid.GridRows;
                GridRows mirror = new GridRows
                {
                    X = reference.X,
                    Y = reference.Y
                };
                mirror.InitializeGrid();
                _gridFactory.CreateMirrorGridRequest(mirror);
                inited = true;
            }
        }
    }
}