using Code.Common.Helpers;
using Code.Meta.UI.HUD.ResourceWindow.Services;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD.ResourceWindow
{
    public class GameResourceController: UpdatableWindow
    {
        [SerializeField] private TextMeshProUGUI resourceText;
        private GameResourceModel _model;
        private IGameResourceService _resourceBarService;
        private IUpdatableWindowService _updatableWindowService;

        [Inject]
        private void Construct(
            IUpdatableWindowService updatableWindowService, 
            IGameResourceService resourceBarService)
        {
            Id = UpdatableWindowId.ResourceWindow;
            _resourceBarService = resourceBarService;
            _updatableWindowService = updatableWindowService;
        }

        protected override void Initialize()
        {
            _model = new GameResourceModel();
            _resourceBarService.SetGameResourceController(this);
        }

        public void SetCurrentResource(int value)
        {
            resourceText.text = StringUpdater.UpdateString(value.ToString("0000"));
        }
        
        protected override void Cleanup()
        {
            _resourceBarService.SetGameResourceController(null);
        }

    }
}