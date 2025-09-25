using System.Collections.Generic;
using System.Linq;
using Code.Audios.Audio.Factory;
using Code.Meta.Achivments.Configs;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.AchivmentsWindow
{
    public class AchivmentWindowController: StaticWindow
    {
        [SerializeField] private Button btnReturn;
        [SerializeField] private RectTransform achivmentsHolder;
        [SerializeField] private AchivmentBar achivmentBarPrefab;
        
        private AchivmentWindowModel _model;
        private IStaticWindowService _staticWindowService;
        private MetaContext _meta;
        private IAudioFactory _audioFactory;


        [Inject]
        private void Construct(
            MetaContext meta,
            IStaticWindowService staticWindowService,
            IAudioFactory audioFactory)
        {
            Id = StaticWindowId.AchivmentsWindow;
            _staticWindowService = staticWindowService;
            _meta = meta;
            _audioFactory = audioFactory;
        }
        
        protected override void Initialize()
        {
            _model = new AchivmentWindowModel(_staticWindowService, _audioFactory);
            
            List<AchivmentData> achivments = _meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.AchivmentsStorage,
                MetaMatcher.Storage
            )).GetEntities().FirstOrDefault()?.AchivmentsStorage;
            
            foreach (AchivmentData data in achivments)
            {
                AchivmentBar bar = Instantiate(achivmentBarPrefab, achivmentsHolder);
                bar.Title.text = data.title;
                bar.Description.text = data.description;
                bar.spritePath = data.iconPath;
                bar.Init(data.achivmentStatusId);
            }
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