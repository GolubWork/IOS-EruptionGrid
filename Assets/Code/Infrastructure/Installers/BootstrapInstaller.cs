using Code.Audios.Audio.Factory;
using Code.Audios.Audio.Services;
using Code.Gameplay.Abilities.Factory;
using Code.Gameplay.Armaments.Factory;
using Code.Gameplay.Backgrounds.Factory;
using Code.Gameplay.Bucket.Factories;
using Code.Gameplay.Buildings.Factories;
using Code.Gameplay.Cameras;
using Code.Gameplay.Cameras.Factory;
using Code.Gameplay.Cards.Factories;
using Code.Gameplay.Cards.Helper;
using Code.Gameplay.Chicken.Factory;
using Code.Gameplay.Common.AABB;
using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Random;
using Code.Gameplay.Common.Time;
using Code.Gameplay.ConveyorBelt.Factories;
using Code.Gameplay.Effects.Factory;
using Code.Gameplay.EffectsVisual.Factories;
using Code.Gameplay.Eggs.Factories;
using Code.Gameplay.Floors.Factories;
using Code.Gameplay.GameLoop;
using Code.Gameplay.GameResource.Factories;
using Code.Gameplay.GridCells.Factories;
using Code.Gameplay.Grids.Factories;
using Code.Gameplay.Items.Factories;
using Code.Gameplay.Levels;
using Code.Gameplay.LevelTimer.Factories;
using Code.Gameplay.Objects.Factories;
using Code.Gameplay.Physics.Factories;
using Code.Gameplay.Shelfs.Factories;
using Code.Gameplay.StaticData.AbilityStaticData;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Gameplay.StaticData.BuildingStaticData;
using Code.Gameplay.StaticData.cardsStaticData;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.EnchantStaticData;
using Code.Gameplay.StaticData.ItemStaticData;
using Code.Gameplay.StaticData.RandomSpriteStaticData;
using Code.Gameplay.StaticData.ShopStaticData;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Gameplay.Statuses.Factory;
using Code.Gameplay.Zones.Factories;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Infrastructure.EntityViews.Fabrics;
using Code.Infrastructure.Identifiers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using Code.Input.Service;
using Code.Meta.Achivments.Factories;
using Code.Meta.Shop.Factories;
using Code.Meta.Shop.Factories.SkinShopFactories;
using Code.Meta.Skins.Factories;
using Code.Meta.UI.HUD.CurrencyContainer.Services;
using Code.Meta.UI.HUD.GoldContainer.Services;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Meta.UI.HUD.PauseWindow.PauseButton.Services;
using Code.Meta.UI.HUD.PauseWindow.Services;
using Code.Meta.UI.HUD.ResourceWindow.Services;
using Code.Meta.UI.HUD.ScoreContainer.Services;
using Code.Meta.UI.HUD.SettingsWindow.Services;
using Code.Meta.UI.HUD.ShopWindow.Providers;
using Code.Meta.UI.HUD.ShopWindow.SkinShop;
using Code.Meta.UI.HUD.TimerWindow.Servises;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        [SerializeField]private LoadingController loadingController;
        
        public override void InstallBindings()
        {
            BindInputService();
            BindAssetManagementServices();
            BindCommonServices();
            BindSystemFactory();
            BindUIFactories();
            BindContexts();
            BindGameplayServices();
            BindAbilityServices();
            BindUIServices();
            BindAuidioServices();
            BindCameraProvider();
            BindGameplayFactories();
            BindStateMachine();
            BindStateFactory();
            BindInfrastructureServices();
            BindGameStates();
            BindProgressServices();
            BindPhysicsFactories();
        }

        private void BindAbilityServices()
        {
            
            Container.Bind<IAbilityStaticDataService>().To<AbilityStaticDataService>().AsSingle();
            Container.Bind<IEnchantStaticDataService>().To<EnchantStaticDataService>().AsSingle();
            Container.Bind<IEffectFactory>().To<EffectFactory>().AsSingle();
            Container.Bind<IArmamentFactory>().To<ArmamentFactory>().AsSingle();
            Container.Bind<IAbilityFactory>().To<AbilityFactory>().AsSingle();
            Container.Bind<IAbilityFXFactory>().To<AbilityFXFactory>().AsSingle();
            Container.Bind<IStatusFactory>().To<StatusFactory>().AsSingle();
        }

        private void BindPhysicsFactories()
        {
            Container.Bind<IForceFactory>().To<ForceFactory>().AsSingle();
        }
        
        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadResourcesState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActualizeProgressState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingHomeScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<HomeScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingBattleState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleEnterState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleLoopState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseState>().AsSingle();
            Container.BindInterfacesAndSelfTo<RestartState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameWinState>().AsSingle();
        }

        private void BindContexts()
        {
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();

            Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
            Container.Bind<AudioContext>().FromInstance(Contexts.sharedInstance.audio).AsSingle();
        }

        private void BindCameraProvider()
        {
            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
        }

        private void BindProgressServices()
        {
            Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();

        }

        private void BindGameplayServices()
        {
            Container.Bind<IWindowsStaticDataService>().To<WindowsStaticDataService>().AsSingle();
            Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().AsSingle();
            Container.Bind<IBattleFeatureService>().To<BattleFeatureService>().AsSingle();
            Container.Bind<IEffectStaticDataService>().To<EffectStaticDataService>().AsSingle();
            Container.Bind<IRandomSpriteProvider>().To<RandomSpriteProvider>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.Bind<IEntityViewFactory<GameEntityBehaviour, GameEntity>>().To<EntityViewFactory<GameEntityBehaviour, GameEntity>>().AsSingle();
            Container.Bind<IEntityViewFactory<AudioEntityBehaviour, AudioEntity>>().To<EntityViewFactory<AudioEntityBehaviour, AudioEntity>>().AsSingle();
            
            Container.Bind<IChickenFactory>().To<ChickenFactory>().AsSingle();
            Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
            Container.Bind<IAudioFactory>().To<AudioFactory>().AsSingle();
            Container.Bind<IFloorFactory>().To<FloorFactory>().AsSingle();
            Container.Bind<IBackgroundFactory>().To<BackgroundFactory>().AsSingle();
            
            Container.Bind<IVisualEffectFactory>().To<VisualEffectFactory>().AsSingle();
            Container.Bind<IVisualEffectStaticDataService>().To<VisualEffectStaticDataService>().AsSingle();
            
            Container.Bind<IPhysicsObjectPool>().To<PhysicsObjectPool>().AsSingle();
            Container.Bind<IDefaultObjectPool>().To<DefaultObjectPool>().AsSingle();
            Container.Bind<IEggFactory>().To<EggFactory>().AsSingle();
            
            Container.Bind<ILevelTimerFactory>().To<LevelTimerFactory>().AsSingle();
            Container.Bind<IAdditionalSpriteProvider>().To<AdditionalSpriteProvider>().AsSingle();
            Container.Bind<IShelfFactory>().To<ShelfFactory>().AsSingle();
            Container.Bind<IAchivmentFactory>().To<AchivmentFactory>().AsSingle();
            
            Container.Bind<IShopDataProvider>().To<ShopDataProvider>().AsSingle();
            Container.Bind<ISkinDataProvider>().To<SkinDataProvider>().AsSingle();
            
            Container.Bind<ISkinShopFactory>().To<SkinShopFactory>().AsSingle();
            Container.Bind<IShopFactory>().To<ShopFactory>().AsSingle();
            Container.Bind<ISkinFactory>().To<SkinFactory>().AsSingle();
            Container.Bind<IShopProvider<SkinShopWindowController>>().To<SkinShopProvider>().AsSingle();
            Container.Bind<IZoneFactory>().To<ZoneFactory>().AsSingle();
            Container.Bind<IBucketFactory>().To<BucketFactory>().AsSingle();
            Container.Bind<IGameResourceFactory>().To<GameResourceFactory>().AsSingle();
            
            Container.Bind<ICardFactory>().To<CardFactory>().AsSingle();
            Container.Bind<ICardSpriteStaticDataProvider>().To<CardSpriteStaticDataProvider>().AsSingle();
            Container.Bind<ICardComparer>().To<CardComparer>().AsSingle();
            
            Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
            Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
            Container.Bind<IBuildingStaticDataProvider>().To<BuildingStaticDataProvider>().AsSingle();
            Container.Bind<IConveyourBeltFactory>().To<ConveyourBeltFactory>().AsSingle();
            Container.Bind<IItemStaticDataService>().To<ItemStaticDataService>().AsSingle();
            
            Container.Bind<IGridFactory>().To<GridFactory>().AsSingle();
            Container.Bind<ICellFactory>().To<CellFactory>().AsSingle();
        }


        private void BindSystemFactory()
        {
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
        }

        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IAssetDownloadReporter>().To<AssetDownloadReporter>().AsSingle();
            Container.Bind<IAssetDownloadService>().To<LabeledAssetDownloadService>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IAABBPhysicsService>().To<AABBPhysicsService>().AsSingle();
        }

        private void BindInputService()
        {
            Container.Bind<ITouchInputService>().To<NewInputService>().AsSingle();
            
            Container.Bind<ICurrentScoreBarService>().To<CurrentScoreBarService>().AsSingle();
            Container.Bind<IBestScoreBarService>().To<BestScoreBarService>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.BindInterfacesAndSelfTo<LoadingController>().FromComponentInNewPrefab(loadingController).AsSingle();
            
            Container.Bind<IStaticWindowService>().To<StaticWindowService>().AsSingle();
            Container.Bind<IUpdatableWindowService>().To<UpdatableWindowService>().AsSingle();
            Container.Bind<IGamePauseButtonService>().To<GamePauseButtonService>().AsSingle();
            Container.Bind<IPauseWindowService>().To<PauseWindowService>().AsSingle();
            
            Container.Bind<ILevelTimerBarService>().To<LevelTimerBarService>().AsSingle();
            Container.Bind<IGameResourceService>().To<GameResourceService>().AsSingle();
        }

        private void BindAuidioServices()
        {
            Container.Bind<ISettingsService>().To<SettingsService>().AsSingle();
        }

        private void BindUIFactories()
        {
            Container.Bind<IStaticWindowFactory>().To<StaticWindowFactory>().AsSingle();
            Container.Bind<IUpdatableWindowFactory>().To<UpdatableWindowFactory>().AsSingle();
            
            Container.Bind<ICurrencyService>().To<CurrencyService>().AsSingle();
            Container.Bind<ICurrencyBarService>().To<CurrencyBarService>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}