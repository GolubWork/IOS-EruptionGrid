using System.Linq;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.Levels.Configs;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.HomeWindow
{
    public class HomeScreenWindow: StaticWindow
    {
        [SerializeField]private Button btnStartGame;
        [SerializeField]private Button btnLevels;
        [SerializeField]private Button btnSettings;
        [SerializeField]private Button btnPrivacy;
        [SerializeField]private Button btnLeaderboard;
        [SerializeField]private Button btnShop;
        [SerializeField]private Button btnAchivments;
        
        private HomeModel _homeModel;
        
        private IGameStateMachine _stateMachine;
        private IAudioFactory _audioFactory;
        private IStaticWindowService _staticWindowService;
        private LevelData _currentLevelData;
        private MetaContext _metaContext;
        
        private UnityAction _onStartGame, _onLevels, _onSettings, _onPrivacy, _onLeaderboard, _onShop, _onAch;
        private LoadingController _loadingWindow;

        [Inject]
        private void Construct(MetaContext metaContext,         
            IGameStateMachine gameStateMachine, 
            IAudioFactory audioFactory,
            IStaticWindowService staticWindowService,
            LoadingController loadingWindow)
        {
            Id = StaticWindowId.HomeWindow;
            _metaContext = metaContext;
            _stateMachine = gameStateMachine;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
            _loadingWindow = loadingWindow;
        }
        
        protected override void Initialize()
        {
            _homeModel = new HomeModel(_stateMachine, _audioFactory, _staticWindowService);
            SetCurrentLevelData();
            LockScreen();
            
            _onStartGame   = OnBtnStartGame;
            _onLevels      = OnBtnLevels;
            _onSettings    = OnBtnSettings;
            _onPrivacy     = OnBtnPrivacy;
            _onLeaderboard = OnBtnLeaderboard;
            _onShop        = _homeModel.Shop;
            _onAch         = _homeModel.Achivments;
        }
        
        protected override void SubscribeUpdates()
        {
            if (btnStartGame)   btnStartGame.onClick.AddListener(_onStartGame);
            if (btnLevels)      btnLevels.onClick.AddListener(_onLevels);
            if (btnSettings)    btnSettings.onClick.AddListener(_onSettings);
            if (btnPrivacy)     btnPrivacy.onClick.AddListener(_onPrivacy);
            if (btnLeaderboard) btnLeaderboard.onClick.AddListener(_onLeaderboard);
            if (btnShop)        btnShop.onClick.AddListener(_onShop);
            if (btnAchivments)  btnAchivments.onClick.AddListener(_onAch);
        }

        protected override void UnsubscribeUpdates()
        {
            if (btnStartGame)   btnStartGame.onClick.RemoveListener(_onStartGame);
            if (btnLevels)      btnLevels.onClick.RemoveListener(_onLevels);
            if (btnSettings)    btnSettings.onClick.RemoveListener(_onSettings);
            if (btnPrivacy)     btnPrivacy.onClick.RemoveListener(_onPrivacy);
            if (btnLeaderboard) btnLeaderboard.onClick.RemoveListener(_onLeaderboard);
            if (btnShop)        btnShop.onClick.RemoveListener(_onShop);
            if (btnAchivments)  btnAchivments.onClick.RemoveListener(_onAch);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _homeModel = null;
        }
        private void SetCurrentLevelData()
        {
            CustomDebug.Log("Getting Levels");
            MetaEntity levelContainer = _metaContext.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.LevelsStorage,
                MetaMatcher.Storage
            )).GetEntities().First();
            
            foreach (LevelData levelData in levelContainer.LevelsStorage)
            {
                if (levelData.levelStatusId == LevelStatusId.Infinity)
                {
                    _currentLevelData = levelData;
                    levelContainer.ReplaceChosenLevel(_currentLevelData);
                }
            }

            if (_currentLevelData == null)
            {
                foreach (LevelData levelData in levelContainer.LevelsStorage)
                {
                    if (levelData.levelStatusId == LevelStatusId.Current)
                    {
                        _currentLevelData = levelData;
                        levelContainer.ReplaceChosenLevel(_currentLevelData);
                    }
                }
            }
        }
        
        private void OnBtnStartGame()
        {
            _loadingWindow.Show();
            btnStartGame.interactable = false;
            _homeModel.EnterBattleLoadingState();
        }
        private void OnBtnLevels()
        {
            _homeModel.Levels();
        }
        private void OnBtnSettings()
        {
            _homeModel.Settings();
        }

        private void OnBtnPrivacy()
        {
            _homeModel.Privacy();
        }
        private void OnBtnLeaderboard()
        {
            _homeModel.Leaderboard();
        }

        private void LockScreen()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
        }
    }
}