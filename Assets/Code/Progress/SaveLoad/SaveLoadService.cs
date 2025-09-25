using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Common.Helpers;
using Code.Infrastructure.Serialization;
using Code.Progress.Data;
using Code.Progress.Provider;
using UnityEngine;

namespace Code.Progress.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string FolderName = "Saves";
    private const string FileSaveName = "Save.json";
    
    private readonly MetaContext _metaContext;
    private readonly AudioContext _audioContext;
    private readonly IProgressProvider _progressProvider;

    public bool HasFileSavedProgress => File.Exists(GetSaveFilePath());
    private string GetSaveFilePath()
    {
      return Path.Combine(Application.persistentDataPath, FolderName, FileSaveName);
    }
    
    public SaveLoadService(
      MetaContext metaContext, 
      AudioContext audioContext,
      IProgressProvider progressProvider)
    {
      _metaContext = metaContext;
      _audioContext = audioContext;
      _progressProvider = progressProvider;
    }

    public void CreateProgress()
    {
      _progressProvider.SetProgressData(new ProgressData()
      {

      });
    }

    public void SaveProgress()
    {
      PreserveMetaEntities();
      PreserveAudioEntities();
      string saveJson = _progressProvider.ProgressData.ToJson();
      SaveProgressToFile(saveJson);
    }


    private void SaveProgressToFile(string saveJson)
    {
      try
      {
        string folderPath = Path.Combine(Application.persistentDataPath, FolderName);
        if (!Directory.Exists(folderPath))
        {
          Directory.CreateDirectory(folderPath);
        }
        string saveFilePath = GetSaveFilePath();
        File.WriteAllText(saveFilePath, saveJson);
        CustomDebug.Log($"Save completed successfully. Save path: {saveFilePath}");
      } 
      catch (Exception ex)
      {
        CustomDebug.LogError($"Failed to save progress: {ex.Message}");
      }
    }
    public void LoadProgress()
    {
       LoadProgressFromFile();
    }

    private void HydrateProgress(string serializedProgress)
    {
      _progressProvider.SetProgressData(serializedProgress.FromJson<ProgressData>());
      HydrateMetaEntities();
      HydrateAudioEntities();
    }



    private void LoadProgressFromFile()
    {
      try
      {
        string saveFilePath = GetSaveFilePath();
        if (File.Exists(saveFilePath))
        {
          string serializedProgress = File.ReadAllText(saveFilePath);
          HydrateProgress(serializedProgress);
#if UNITY_EDITOR
          CustomDebug.Log($"Load completed successfully from: {saveFilePath}");
#endif
        }
        else
        {
          CreateProgress();
#if UNITY_EDITOR
          CustomDebug.LogWarning("Save file not found, initializing new progress.");
#endif
        }
      }
      catch (Exception ex)
      {
        CustomDebug.LogError($"Failed to load progress: {ex.Message}");
        CreateProgress();
      }
    }

    private void HydrateMetaEntities()
    {
      List<EntitySnapshot> snapshots = _progressProvider.EntityData.MetaEntitySnapshots;
      foreach (EntitySnapshot snapshot in snapshots)
      {
        _metaContext
          .CreateEntity()
          .HydrateWith(snapshot);
      }
    }
    private void HydrateAudioEntities()
    {
      List<EntitySnapshot> snapshots = _progressProvider.EntityData.AudioEntitySnapshots;
      foreach (EntitySnapshot snapshot in snapshots)
      {
        _audioContext
          .CreateEntity()
          .HydrateWith(snapshot);
      }
    }
    private void PreserveMetaEntities()
    {
      _progressProvider.EntityData.MetaEntitySnapshots = _metaContext
        .GetEntities()
        .Where(RequiresSave)
        .Select(e => e.AsSavedEntity())
        .ToList();
    }
    


    private void PreserveAudioEntities()
    {
      _progressProvider.EntityData.AudioEntitySnapshots = _audioContext
        .GetEntities()
        .Where(RequiresSave)
        .Select(e => e.AsSavedEntity())
        .ToList();
    }
    private static bool RequiresSave(MetaEntity e) => e.GetComponents().Any(c => c is ISavedComponent);
    private static bool RequiresSave(AudioEntity e) => e.GetComponents().Any(c => c is ISavedComponent);
  }
}