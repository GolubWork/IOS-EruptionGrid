using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadSceneAddressable(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadAdresable(name, onLoaded));
        public void LoadSceneBuildIn(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadBuildIn(name, onLoaded));

        private IEnumerator LoadAdresable(string nextScene, Action onLoaded)
        {
            AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(nextScene);

            while (!waitNextScene.IsDone)
                yield return null;

            onLoaded?.Invoke();
        }        
        
        private IEnumerator LoadBuildIn(string nextScene, Action onLoaded)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (waitNextScene != null && !waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}