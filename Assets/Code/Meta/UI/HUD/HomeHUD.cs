using Code.Windows.StaticWindows;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD
{
    public class HomeHUD : MonoBehaviour
    {
        private IStaticWindowService _staticWindowService;

        [Inject]
        private void Construct(IStaticWindowService staticWindowService)
        {
            _staticWindowService = staticWindowService;
        }

        private void Start()
        {
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }
    }
}