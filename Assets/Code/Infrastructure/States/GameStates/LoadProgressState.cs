using System;
using System.Collections.Generic;
using Code.Audios.Audio.Configs;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Common.Helpers;
using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.Achivments.Configs;
using Code.Meta.Achivments.Factories;
using Code.Meta.Levels.Configs;
using Code.Meta.Skins.Factories;
using Code.Progress.SaveLoad;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadProgressState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IWindowsStaticDataService _windowsStaticDataService;
    private readonly IAchivmentFactory _achivmentFactory;
    private readonly IAssetProvider _assetProvider;
    private readonly ISkinFactory _skinFactory;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(
      IGameStateMachine stateMachine,
      ISaveLoadService saveLoadService,
      IWindowsStaticDataService windowsStaticDataService,
      IAchivmentFactory achivmentFactory,
      IAssetProvider assetProvider,
      ISkinFactory skinFactory)
    {
      _saveLoadService = saveLoadService;
      _stateMachine = stateMachine;
      _windowsStaticDataService = windowsStaticDataService;
      _achivmentFactory = achivmentFactory;
      _assetProvider = assetProvider;
      _skinFactory = skinFactory;
    }
    
    public override async void Enter()
    {
      await InitializeProgress();
      _stateMachine.Enter<ActualizeProgressState>();
    }


    private async UniTask InitializeProgress()
    {
      if (_saveLoadService.HasFileSavedProgress)
        _saveLoadService.LoadProgress();
      else
        await CreateNewProgress();
    }

    private async UniTask CreateNewProgress()
    {
      _saveLoadService.CreateProgress();

      CreateMetaEntity.Empty()
        .With(e => e.isStorage = true)
        .AddSessionScore(0)
        .AddBestScore(0);

      CreateMetaEntity.Empty()
        .With(e => e.isStorage = true)
        .AddSessionCurrency(0)
        .AddCurrencyStorage(0);
      
      await LoadLevels();
      await LoadAchivments();

      CreateAudioEntity.Empty()
        .AddAudioSettings(new AudioSettingsData()
        {
          MusicVolume = 1,
          SoundVolume = 1
        });

      _skinFactory.InitSelectedSkin();
    }

    private async UniTask LoadLevels()
    {
        CustomDebug.Log("Loading Levels");
        LevelConfigs levelConfigs = await _assetProvider.LoadScriptable<LevelConfigs>(ConfigsDirectoryConstants.LevelConfigPath);
        List<LevelData> levels = new();
        foreach (LevelData level in levelConfigs.Levels)
        {
          levels.Add(level);
        }
        CreateMetaEntity.Empty()
          .With(e => e.isStorage = true)
          .AddLevelsStorage(levels);
        CustomDebug.Log("Loading Levels finish");
    }
    
    private async UniTask LoadAchivments()
    {
      
      AchivmentConfig achivmentConfig = await _assetProvider.LoadScriptable<AchivmentConfig>(ConfigsDirectoryConstants.AchivmentsConfigPath);
      List<AchivmentData> achivments = new();
      foreach (AchivmentData achivment in achivmentConfig.Achivments)
      {
          achivments.Add(achivment);
      }
      CreateMetaEntity.Empty()
        .With(e => e.isStorage = true)
        .AddAchivmentsStorage(achivments);
      
      _achivmentFactory.CreateTapCounter();
      _achivmentFactory.CreateGoldCounter();
    }
  }
}