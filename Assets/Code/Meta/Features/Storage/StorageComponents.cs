using Code.Progress;
using Entitas;

namespace Code.Meta.Features.Storage
{

    [Meta] public class GoldStorage : ISavedComponent { }
    [Meta] public class MaxGold : ISavedComponent { public float Value; }
    [Meta] public class CurrentGold : ISavedComponent { public float Value; }
}