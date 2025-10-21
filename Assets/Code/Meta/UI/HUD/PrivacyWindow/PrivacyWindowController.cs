using Code.Audios.Audio.Factory;
using Code.Infrastructure.DependencyInjection;
using Code.Windows.StaticWindows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.PrivacyWindow
{
    public class PrivacyWindowController: StaticWindow
    {
        [SerializeField] private Button btnReturn;
        [SerializeField] private TextMeshProUGUI privacyTextContainer;
        
        private PrivacyWindowModel _model;
        private IStaticWindowService _staticWindowService;
        private IAudioFactory _audioFactory;


        [Inject]
        private void Construct(
            IStaticWindowService staticWindowService,
            IAudioFactory audioFactory)
        {
            Id = StaticWindowId.PrivacyWindow;
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
        }
        
        protected override void Initialize()
        {
            _model = new PrivacyWindowModel(_staticWindowService, _audioFactory);
            _model.SetPrivacyText(privacyTextContainer);
        }
        
        protected override void SubscribeUpdates()
        {
            btnReturn.onClick.AddListener(_model.ReturnHome);
        }

        protected override void UnsubscribeUpdates()
        {
            btnReturn.onClick.RemoveListener(_model.ReturnHome);
        }
        private void OnDisable()
        {
            UnsubscribeUpdates();
        }
    }
}