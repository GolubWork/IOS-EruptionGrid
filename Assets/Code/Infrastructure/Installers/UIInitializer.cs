using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Infrastructure.DependencyInjection;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour
  {
    [SerializeField] private RectTransform uiRoot;

    [Inject] IStaticWindowFactory _staticFactory;
    [Inject] IUpdatableWindowFactory _updatableFactory;

    private void Awake()
    {
      ServiceProvider.Inject(this);
      _staticFactory.SetUiRoot(uiRoot.gameObject);
      _updatableFactory.SetUiRoot(uiRoot.gameObject);
    }
  }
}