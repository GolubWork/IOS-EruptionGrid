using System.Linq;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.Levels.Configs;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        [Inject]
        private void Construct(MetaContext metaContext,         
            IGameStateMachine gameStateMachine, 
            IAudioFactory audioFactory,
            IStaticWindowService staticWindowService)
        {
            Id = StaticWindowId.HomeWindow;
            _metaContext = metaContext;
            _stateMachine = gameStateMachine;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
        }
        
        protected override void Initialize()
        {
            _homeModel = new HomeModel(_stateMachine, _audioFactory, _staticWindowService);
            SetCurrentLevelData();
            LockScreen();
        }
        
        protected override void SubscribeUpdates()
        {
            btnStartGame.onClick.AddListener(OnBtnStartGame);
            btnLevels.onClick.AddListener(OnBtnLevels);
            btnSettings.onClick.AddListener(OnBtnSettings);
            btnPrivacy.onClick.AddListener(OnBtnPrivacy);
            btnLeaderboard.onClick.AddListener(OnBtnLeaderboard);
            btnShop.onClick.AddListener(_homeModel.Shop);
            btnAchivments.onClick.AddListener(_homeModel.Achivments);
        }



        protected override void UnsubscribeUpdates()
        {
            btnStartGame.onClick.RemoveListener(OnBtnStartGame);
            btnLevels.onClick.RemoveListener(OnBtnLevels);
            btnSettings.onClick.RemoveListener(OnBtnSettings);
            btnPrivacy.onClick.RemoveListener(OnBtnPrivacy);
            btnLeaderboard.onClick.RemoveListener(OnBtnLeaderboard);
            btnShop.onClick.RemoveListener(_homeModel.Shop);
            btnAchivments.onClick.RemoveListener(_homeModel.Achivments);
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