using _Scripts.GridSpawn;
using UnityEngine;

namespace Code.Gameplay.Grids.Factories
{
    public interface IGridFactory
    {
        GameEntity CreateReferenceGrid(GridRows gridRows);
        GameEntity CreateReferenceGridRequest(GridRows gridRows);
        GameEntity CreateMirrorGridRequest(GridRows chosenLevelGrid);

        GameEntity CreateMirrorGrid(GridRows gridRows);
    }
}