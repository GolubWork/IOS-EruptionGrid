using Code.Audios.Audio.Factory;
using Code.Windows.StaticWindows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.PrivacyWindow
{
    public class PrivacyWindowController: StaticWindow
    {
        [SerializeField] private Button btnReturn;
        [SerializeField] private TextMeshProUGUI privacyTextContainer;
        [SerializeField] private string privacyText;
        
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
            _model.SetPrivacyText(privacyTextContainer, privacyText);
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