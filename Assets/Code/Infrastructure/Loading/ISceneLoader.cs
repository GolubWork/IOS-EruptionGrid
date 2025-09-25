using System;

namespace Code.Infrastructure.Loading
{
    public interface ISceneLoader
    {
        void LoadSceneAddressable(string name, Action onLoaded = null);
        void LoadSceneBuildIn(string name, Action onLoaded = null);
    }
}