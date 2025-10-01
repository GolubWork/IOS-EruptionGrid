using Code.Common.Helpers;
using Code.Infrastructure.DependencyInjection;
using Code.Windows.StaticWindows;
using UnityEngine;

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
            CustomDebug.Log("Home HUD");
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }
    }
}