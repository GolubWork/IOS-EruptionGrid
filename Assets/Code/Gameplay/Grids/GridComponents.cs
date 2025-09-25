using _Scripts.GridSpawn;
using Entitas;

namespace Code.Gameplay.Grids
{
    [Game] public class Grid : IComponent {  }
    [Game] public class PlayerGrid : IComponent { }
    [Game] public class ReferenceGrid : IComponent { }
    [Game] public class GridRowsComponent : IComponent { public GridRows Value;  }
    [Game] public class ReferenceGridBuildRequest : IComponent {  }
    [Game] public class PlayerGridBuildRequest : IComponent {  }
    [Game] public class LinkedGridId : IComponent { public int Value; }


}