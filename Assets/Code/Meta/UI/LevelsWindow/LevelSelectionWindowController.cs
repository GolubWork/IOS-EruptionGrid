using System.Collections.Generic;
using System.Linq;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta;
using Code.Meta.Levels.Configs;
using Code.Meta.UI.HUD.LevelsWindow;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelSelectionWindowController : StaticWindow
{
        [SerializeField] private LevelBar _levelBarPrefab;
        [SerializeField] private RectTransform _LevelBarContainer;
        [SerializeField] private Button _btnRetturn;
        
        private LevelSelectionWindowModel _model;
        private MetaContext _metaContext;
        private MetaEntity _levelContainer;

        private List<LevelBar> levelBars = new ();
        private IStaticWindowService _staticWindowService;
        private IAudioFactory _audioFactory;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(
            MetaContext metaContext,
            IStaticWindowService staticWindowService,
            IAudioFactory audioFactory,
            IGameStateMachine gameStateMachine)
        {
            Id = StaticWindowId.LevelSelectionWindow;
            _metaContext = metaContext;
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
            _gameStateMachine = gameStateMachine;
        }

        protected override void Initialize()
        {
            _model = new LevelSelectionWindowModel(_metaContext);
            
            _levelContainer = _metaContext.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.LevelsStorage
            )).GetEntities().First();


        }
        protected override void SubscribeUpdates()
        {
            _btnRetturn.onClick.AddListener(OnReturn);
            foreach (LevelData levelData in _levelContainer.LevelsStorage)
            {
                if(levelData.levelStatusId == LevelStatusId.Infinity) continue;
                LevelBar levelBar = Instantiate(_levelBarPrefab, _LevelBarContainer);
                levelBars.Add(levelBar);
                levelBar.InitBar(levelData);
                levelBar.OnLevelSelected += OnLevelSelectedHandler;
            }
        }

        private void OnReturn()
        {
            _staticWindowService.Close(Id);
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }

        protected override void UnsubscribeUpdates()
        {
            _btnRetturn.onClick.RemoveListener(OnReturn);
            foreach (LevelBar levelBar in levelBars)
            {
                levelBar.OnLevelSelected -= OnLevelSelectedHandler;
            }
        }
        
        
        protected override void Cleanup()
        {
            base.Cleanup();
            _metaContext = null;
            _levelContainer = null;
        }

        private void OnLevelSelectedHandler(LevelData levelData)
        {
            _levelContainer.ReplaceChosenLevel(levelData);
            EnterBattleLoadingState();
        }
        
        private void EnterBattleLoadingState()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.CloseAll();
            _gameStateMachine.Enter<LoadingBattleState, string>(MetaConstants.BattleSceneName);
        }
}
