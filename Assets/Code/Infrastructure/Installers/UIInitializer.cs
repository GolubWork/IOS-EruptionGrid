using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    private IStaticWindowFactory _staticWindowFactory;
    
    public RectTransform UIRoot;
    private IUpdatableWindowFactory _updatableWindowFactory;
    private IWindowsStaticDataService _staticWindowService;

    [Inject]
    private void Construct(
      IStaticWindowFactory staticWindowFactory, 
      IUpdatableWindowFactory updatableWindowFactory, 
      IWindowsStaticDataService staticWindowService)
    {
      _staticWindowFactory = staticWindowFactory;
      _updatableWindowFactory = updatableWindowFactory;
      _staticWindowService = staticWindowService;
    }
    
    public void Initialize()
    {
      _staticWindowFactory.SetUIRoot(UIRoot);
      _updatableWindowFactory.SetUiRoot(_staticWindowService.GetCanvasPrefab());
    }
     
  }
}