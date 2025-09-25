using System;
using Code.Audios.Audio.Services;
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
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
    public class LoadResourcesState : SimpleState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAssetDownloadService _downloadService;
        private readonly IWindowsStaticDataService _windowsStaticDataService;
        private readonly IAbilityStaticDataService _abilityStaticDataService;
        private readonly IEnchantStaticDataService _enchantStaticDataService;
        private readonly IAudioService _audioService;
        private readonly ISkinDataProvider _skinDataProvider;
        private readonly IShopDataProvider _shopDataProvider;
        private readonly IEffectStaticDataService _effectsStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IRandomSpriteProvider _randomSpriteProvider;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly ICardSpriteStaticDataProvider _cardSpriteStaticDataProvider;
        private readonly IBuildingStaticDataProvider _buildingStaticDataProvider;
        private readonly IItemStaticDataService _itemStaticDataService;


        public LoadResourcesState(
            IGameStateMachine stateMachine,
            IAssetDownloadService downloadService,
            IWindowsStaticDataService windowsStaticDataService,
            IAbilityStaticDataService abilityStaticDataService,
            IEnchantStaticDataService enchantStaticDataService,
            IEffectStaticDataService effectsStaticDataService,
            IVisualEffectStaticDataService visualEffectStaticDataService,
            IRandomSpriteProvider randomSpriteProvider,
            IAdditionalSpriteProvider additionalSpriteProvider,
            IAudioService audioService,
            ISkinDataProvider skinDataProvider,
            IShopDataProvider shopDataProvider,
            ICardSpriteStaticDataProvider cardSpriteStaticDataProvider,
            IBuildingStaticDataProvider buildingStaticDataProvider,
            IItemStaticDataService itemStaticDataService
        )
        {
            _stateMachine = stateMachine;
            _downloadService = downloadService;
            _windowsStaticDataService = windowsStaticDataService;
            _abilityStaticDataService = abilityStaticDataService;
            _enchantStaticDataService = enchantStaticDataService;
            _effectsStaticDataService = effectsStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
            _randomSpriteProvider = randomSpriteProvider;
            _additionalSpriteProvider = additionalSpriteProvider;
            _audioService = audioService;
            _skinDataProvider = skinDataProvider;
            _shopDataProvider = shopDataProvider;
            _cardSpriteStaticDataProvider = cardSpriteStaticDataProvider;
            _buildingStaticDataProvider = buildingStaticDataProvider;
            _itemStaticDataService = itemStaticDataService;
        }

        public override async void Enter()
        {
            await LoadResources();
            
            _stateMachine.Enter<LoadProgressState>();
        }

        private async UniTask LoadResources()
        {
            await LoadContent();
            await _windowsStaticDataService.LoadAll();
            await _audioService.LoadAll();
            await _abilityStaticDataService.LoadAll();
            await _enchantStaticDataService.LoadAll();
            await _effectsStaticDataService.LoadAll();
            await _visualEffectStaticDataService.LoadAll();
            await _randomSpriteProvider.LoadAll();
            await _additionalSpriteProvider.LoadAll();
            await _skinDataProvider.LoadAll();
            await _shopDataProvider.LoadAll();
            await _cardSpriteStaticDataProvider.LoadAll();
            await _buildingStaticDataProvider.LoadAll();
            await _itemStaticDataService.LoadAll();
        }
        private async UniTask LoadContent()
        {
            await _downloadService.InitializeDownloadDataAsync();
            float downloadSize = _downloadService.GetDownloadSizeMb();
            Debug.Log($"Download size: {downloadSize} Mb");

            if (downloadSize > 0)
                await _downloadService.UpdateContentAsync();
        }
    }
}