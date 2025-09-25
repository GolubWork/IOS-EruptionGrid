using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.StaticData.WindowsStaticData
{
    public interface IWindowsStaticDataService
    {
        UniTask LoadAll();
        GameObject GetStaticWindowPrefab(StaticWindowId id);
        GameObject GetUpdatableWindowPrefab(UpdatableWindowId id);
        GameObject GetCanvasPrefab();

    }
}