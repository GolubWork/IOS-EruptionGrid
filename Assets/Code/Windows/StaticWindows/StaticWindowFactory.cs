using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Infrastructure.DependencyInjection;
using UnityEngine;

namespace Code.Windows.StaticWindows
{
  public class StaticWindowFactory : IStaticWindowFactory
  {
    private readonly IWindowsStaticDataService _windowsStaticData;
    private readonly IInstantiator _instantiator;

    private RectTransform _uiRoot;

    public StaticWindowFactory(IWindowsStaticDataService windowsStaticData, IInstantiator instantiator)
    {
      _windowsStaticData = windowsStaticData;
      _instantiator = instantiator;
    }
    public void SetUiRoot(GameObject uiRoot)=> _uiRoot = uiRoot.GetComponent<RectTransform>();

    public StaticWindow CreateWindow(StaticWindowId id)
    {
      var prefab = _windowsStaticData.GetStaticWindowPrefab(id);
      // важно: parent и worldPositionStays=false для корректной верстки
      return _instantiator.InstantiatePrefabForComponent<StaticWindow>(prefab, _uiRoot.transform, false);
    }
  }
}