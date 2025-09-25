using System.Collections.Generic;
using Code.Meta.Levels.Configs;
using Code.Progress;

namespace Code.Meta.Levels
{
    [Meta] public class LevelsStorage : ISavedComponent { public List<LevelData> Value; }
    [Meta] public class ChosenLevel : ISavedComponent { public LevelData Value; }
}