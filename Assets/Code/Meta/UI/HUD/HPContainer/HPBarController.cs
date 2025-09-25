using Code.Meta.UI.HUD.HPContainer.Services;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.HPContainer
{
    public class HPBarController: StaticWindow
    {
        [SerializeField] private Slider HealthBar;
        [SerializeField] private Image Fill;

        private HPBarModel _model;
        private IStaticWindowService _staticWindowService;
        private IHPBarService _hpBarService;

        [Inject]
        private void Construct(IStaticWindowService staticWindowService, IHPBarService hpBarService)
        {
           // Id = WindowId.HpBarWindow;
            _model = new HPBarModel(HealthBar, Fill);
            _hpBarService = hpBarService;
            _staticWindowService = staticWindowService;
        }

        protected override void Initialize()
        {
            _hpBarService.SetHPBar(this);
        }

        public void SetHealth(float heroHp, float maxHp)
        {
            _model.SetHealth(heroHp, maxHp);
        }
        
        protected override void Cleanup()
        {
            _hpBarService.SetHPBar(null);
            _staticWindowService.Close(Id);
        }
    }
}