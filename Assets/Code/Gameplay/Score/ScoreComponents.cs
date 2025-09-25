using Code.Meta.UI.HUD.ScoreContainer;
using Code.Progress;
using Entitas;

namespace Code.Gameplay.Score
{
    [Meta] public class SessionScore : ISavedComponent { public float Value; }
    [Meta] public class BestScore : ISavedComponent { public float Value; }
    [Meta] public class ScoreStorage : ISavedComponent { }
    [Game] public class ScoreContains : IComponent { public float Value; }
    
    [Game] public class CurrenScoreContainer : IComponent { public BestScoreBarController Value; }
    [Game] public class MaxScoreContainer : IComponent { public BestScoreBarController Value; }
}