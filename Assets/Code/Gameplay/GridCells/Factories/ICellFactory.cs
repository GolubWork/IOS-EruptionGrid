using UnityEngine;

namespace Code.Gameplay.GridCells.Factories
{
    public interface ICellFactory
    {
        GameEntity CreateGridCell(Vector3 at);
        GameEntity CreateCellFill(Vector3 at, int cellId);
        void ReturnCellFill(GameEntity filler);
        GameEntity CreateMirrorGridCell(Vector3 vector3);
    }
}