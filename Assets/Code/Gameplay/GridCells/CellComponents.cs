using Entitas;
using UnityEngine;

namespace Code.Gameplay.GridCells
{
    [Game] public class GridCell : IComponent {  }
    [Game] public class InteractableCell : IComponent {  }
    [Game] public class ActiveCell : IComponent {  }
    [Game] public class CellFiller : IComponent {  }
    [Game] public class ActiveCellProcessed : IComponent {  }
    [Game] public class ReferenceCell : IComponent {  }
    [Game] public class PlayerCell : IComponent {  }
    [Game] public class LinkedCellId : IComponent { public int Value; }
    [Game] public class CellGridCoordinates : IComponent { public Vector2Int Value; }
    
    
}