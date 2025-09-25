using Code.Gameplay.Grids.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.GridCells
{
    public class CellFeature: Feature
    {
        public CellFeature(ISystemFactory systems)
        {
            Add(systems.Create<SpawnReferenceCellsOnGridSystem>());
            Add(systems.Create<SpawnMirrorCellsOnGridSystem>());
            Add(systems.Create<SpawnFillerOnActiveCellSystem>());
            Add(systems.Create<PlayerGridsCellMatcher>());
            Add(systems.Create<RemoveFillerOnNonActiveCellSystem>());
            Add(systems.Create<GridsComparer>());
        }
    }
}