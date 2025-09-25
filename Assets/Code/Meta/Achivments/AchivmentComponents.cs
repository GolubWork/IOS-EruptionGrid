using System.Collections.Generic;
using Code.Meta.Achivments.Configs;
using Code.Progress;
using Entitas;

namespace Code.Meta.Achivments
{
    [Meta] public class AchivmentsStorage : ISavedComponent { public List<AchivmentData> Value; }
    
    [Meta] public class TapCounter : ISavedComponent { }
    [Meta] public class TapCounted : ISavedComponent { public int Value; }
    [Meta] public class TapTargetCount : ISavedComponent { public int Value; }
    
    [Meta] public class GoldCounter : ISavedComponent { }
    [Meta] public class GoldTargetCount : ISavedComponent { public int Value; }
}