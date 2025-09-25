using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
       UniTask<T> LoadAsset<T>(string path) where T : Component;
       UniTask<T> LoadScriptable<T>(string path) where T : ScriptableObject;
       UniTask<Dictionary<string, T>> LoadAllScriptable<T>(string label) where T : ScriptableObject;
       UniTask<string[]> LoadPrefabNames(string path);
    }
}