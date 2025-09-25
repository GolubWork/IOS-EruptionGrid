using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Helpers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {

        public async UniTask<T> LoadAsset<T>(string path) where T : Component
        {
            try
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(path);
                var prefab = await handle.ToUniTask();
                
                if (prefab.TryGetComponent<T>(out var component))
                {
                    return component;
                }
                else
                {
                    CustomDebug.LogError($"[LoadAsset] Component of type {typeof(T)} not found in prefab at path: {path}");
                    return null;
                }
            }
            catch (InvalidKeyException ex)
            {
                CustomDebug.LogError($"[LoadAsset] Invalid key or type mismatch for path: {path}. Exception: {ex}");
                return null;
            }
            catch (Exception ex)
            {
                CustomDebug.LogError($"[LoadAsset] An error occurred while loading asset from path: {path}. Exception: {ex}");
                return null;
            }
        }

        public async UniTask<T> LoadScriptable<T>(string path) where T : ScriptableObject
        {
            try
            {
                return await Addressables.LoadAssetAsync<T>(path).ToUniTask();
            }
            catch (InvalidKeyException ex)
            {
                CustomDebug.LogError($"[LoadScriptable] Invalid key or type mismatch for path: {path}. Exception: {ex}");
                return null;
            }
            catch (Exception ex)
            {
                CustomDebug.LogError($"[LoadScriptable] An error occurred while loading scriptable object from path: {path}. Exception: {ex}");
                return null;
            }
        }
        public async UniTask<Dictionary<string, T>> LoadAllScriptable<T>(string label) where T : ScriptableObject
        {
            try
            {
                var handle = Addressables.LoadAssetsAsync<T>(label, null);
                IList<T> assets = await handle.ToUniTask();
                var assetDictionary = assets.ToDictionary(asset => asset.name, asset => asset);
                Addressables.Release(handle);
                return assetDictionary;
            }
            catch (System.Exception ex)
            {
                CustomDebug.LogError($"[LoadAllScriptable] Error loading assets with label: {label}. Exception: {ex}");
                return new Dictionary<string, T>();
            }
        }

        public async UniTask<string[]> LoadPrefabNames(string label)
        {
            try
            {
                var handle = Addressables.LoadAssetsAsync<GameObject>(label, null);
                IList<GameObject> prefabs = await handle.ToUniTask();
                
                string[] prefabNames = prefabs.Select(prefab => prefab.name).ToArray();
                
                Addressables.Release(handle);

                return prefabNames;
            }
            catch (InvalidKeyException ex)
            {
                CustomDebug.LogError($"[LoadPrefabNames] Invalid key or type mismatch for label: {label}. Exception: {ex}");
                return Array.Empty<string>();
            }
            catch (Exception ex)
            {
                CustomDebug.LogError($"[LoadPrefabNames] An error occurred while loading prefabs for label: {label}. Exception: {ex}");
                return Array.Empty<string>();
            }
        }
    }
}