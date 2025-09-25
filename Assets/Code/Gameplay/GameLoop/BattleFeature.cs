using Code.Audios.Audio;
using Code.Common.Destruct;
using Code.Gameplay.Backgrounds;
using Code.Gameplay.Cameras;
using Code.Gameplay.Cards;
using Code.Gameplay.Collection;
using Code.Gameplay.EffectApplication;
using Code.Gameplay.Effects;
using Code.Gameplay.GameOver.Systems;
using Code.Gameplay.GameResource;
using Code.Gameplay.GridCells;
using Code.Gameplay.Grids;
using Code.Gameplay.LifeTime;
using Code.Gameplay.Movement;
using Code.Gameplay.Objects;
using Code.Gameplay.Score;
using Code.Gameplay.Taps;
using Code.Infrastructure.EntityViews;
using Code.Infrastructure.Systems;
using Code.Input;
using Code.Meta.Achivments;
using Code.Meta.Currency;
using Code.Meta.Skins;

namespace Code.Gameplay.GameLoop
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory systems)
        {
            NormalInit(systems);
        }

        private void NormalInit(ISystemFactory systems)
        {
            Add(systems.Create<InputFeature>());
            Add(systems.Create<BindViewFeature>());
            Add(systems.Create<ObjectPoolFeature>());

            Add(systems.Create<BackgroundFeature>());
            
            Add(systems.Create<GameResourceFeature>());
            Add(systems.Create<CurrencyFeature>());
            Add(systems.Create<CollectionFeature>());
            Add(systems.Create<SkinFeature>());
            Add(systems.Create<TapFeature>());
            
            Add(systems.Create<EffectApplicationFeature>());
            
            
            Add(systems.Create<EffectFeature>());
            Add(systems.Create<DeathFeature>());
            
            Add(systems.Create<CameraFeature>());
            Add(systems.Create<MovementFeature>());
            Add(systems.Create<GridFeature>());
            Add(systems.Create<CellFeature>());
            Add(systems.Create<ScoreFeature>());
            
            Add(systems.Create<GameOverOnZeroGameResourceSystem>());
            Add(systems.Create<AudioFeature>());
            Add(systems.Create<ProcessDestructedFeature>());
            Add(systems.Create<AchivmentFeature>());
        }


    }
}