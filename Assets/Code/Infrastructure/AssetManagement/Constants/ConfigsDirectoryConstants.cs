namespace Code.Infrastructure.AssetManagement.Constants
{
    public class ConfigsDirectoryConstants
    {
        public const string MusicConfigSourcePath = "Configs/Audio/MusicConfig.asset";
        public const string SoundConfigSourcePath = "Configs/Audio/SoundConfig.asset";
        
        public const string StaticWindowsPath = "Configs/Windows/StaticWindowConfig.asset";
        public const string UpdatableWindowsPath = "Configs/Windows/UpdatableWindowConfig.asset";
        
        public const string LevelConfigPath = "Configs/Levels/LevelConfig.asset";
        public const string ShopConfigPath = "Configs/Shop/ShopConfig.asset";
        public const string SkinConfigPath = "Configs/Skin/SkinConfig.asset";
        public const string AchivmentsConfigPath = "Configs/Achivments/AchivmentsConfig.asset";
        
        public const string RandomSpriteConfig = "Configs/Sprites/RandomSpriteConfig.asset";
        public const string AdditionalSpriteConfig = "Configs/Sprites/AdditionalSpriteConfig.asset";
        
        public const string BuildingConfig = "Configs/Buildings/BuildingConfigs.asset";
        public const string ItemConfig = "Configs/Items/ItemConfigs.asset";
        
        public const string VisualEffects = "Configs/VisualEffects/VisualEffectConfigs.asset";
        public const string CardConfigs = "Configs/Cards/CardConfigs.asset";
    }
    
    public class ScenesDirectoryConstants
    {
        public const string HomeScreenPath = "HomeScreen";
        public const string LoadingScenePath = "LoadingScene";
    }    
    
    public class PrefabsDirectoryConstants
    {
        public const string AudioListenerPrefabSourcePath = "Gameplay/AudioSources/AudioListener.prefab";
        public const string MusicPrefabSourcePath = "Gameplay/AudioSources/MusicSource.prefab";
        public const string SoundPrefabSourcePath = "Gameplay/AudioSources/SoundSource.prefab";
        
        public const string ChickenPrefabPath = "Gameplay/Chicken/Chicken.prefab";
        public const string FloorPrefabPath = "Gameplay/Floors/Floor.prefab";
        
        public const string MainCameraPrefabPath = "Gameplay/Cameras/MainCamera.prefab";
        public const string BorderCameraPrefabPath = "Gameplay/Cameras/BorderCamera.prefab";
        
        public const string BacgroundPrefabPath = "Gameplay/Backgrounds/Background.prefab";
        
        public const string DefaultObjectPrefabPath = "Gameplay/ObjectPooling/Object.prefab";
        public const string DefaultPhysicsObjectPrefabPath = "Gameplay/ObjectPooling/PhysicsObject.prefab";
        
        public const string ZonePrefabPath = "Gameplay/Zones/Zone.prefab";
        
        public const string ConveuourBelt = "Gameplay/Conveyour.prefab";
    }

    public class DownloadServiceConstants
    {
        public const string LocalLabel = "local";
        public const string AbilitiesLabel = "ability";
        public const string EnchantsLabel = "enchant";
        public const string EffectsLabel = "effect";
    }
}