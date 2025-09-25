using Code.Gameplay.StaticData.WindowsStaticData;
using UnityEngine;
using Zenject;

namespace Code.Windows.StaticWindows
{
  public class StaticWindowFactory : IStaticWindowFactory
  {
    private readonly IWindowsStaticDataService _windowsStaticData;
    private readonly IInstantiator _instantiator;
    private RectTransform _uiRoot;

    public StaticWindowFactory(
      IWindowsStaticDataService windowsStaticData,
      IInstantiator instantiator)
    {
      _windowsStaticData = windowsStaticData;
      _instantiator = instantiator;
    }

    public void SetUIRoot(RectTransform uiRoot) =>
      _uiRoot = uiRoot;

    public StaticWindow CreateWindow(StaticWindowId staticWindowId) =>
      _instantiator.InstantiatePrefabForComponent<StaticWindow>(PrefabFor(staticWindowId), _uiRoot);

    private GameObject PrefabFor(StaticWindowId id) =>
      _windowsStaticData.GetStaticWindowPrefab(id);
  }
}