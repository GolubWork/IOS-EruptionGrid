using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.GameOverWindow
{
    public class GameOverWindow : StaticWindow
    {
        [SerializeField] private Button btnReturnHome;
        [SerializeField] private Button btnRestart;
        
        [SerializeField] private TextMeshProUGUI currentScore;
        [SerializeField] private TextMeshProUGUI maxScore;
        
        private GameOverWindowModel _model;
        
        private IGameStateMachine _stateMachine;
        private IStaticWindowService _staticWindowService;
        private IAudioFactory _audioFactory;
        private ISceneLoader _sceneLoader;
        private IUpdatableWindowService _updatableWindowService;

        [Inject]
        private void Construct(
            MetaContext meta,
            IGameStateMachine stateMachine, 
            IStaticWindowService staticWindowService, 
            IAudioFactory audioFactory,
            ISceneLoader sceneLoader,
            IUpdatableWindowService updatableWindowService)
        {
            Id = StaticWindowId.GameOverWindow;
            
            _stateMachine = stateMachine;
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
            _sceneLoader = sceneLoader;
            _updatableWindowService = updatableWindowService;

            SetScoreToUI(meta);
        }

        private void SetCurrencyToUI(MetaContext meta)
        {
            MetaEntity currencyStorage = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionCurrency,
                MetaMatcher.CurrencyStorage
            )).GetSingleEntity();
            
            UpdateCurrentScore(currencyStorage.SessionCurrency, "Gold");
            currencyStorage.ReplaceCurrencyStorage(currencyStorage.CurrencyStorage + currencyStorage.SessionCurrency);
            UpdateBestScore(currencyStorage.CurrencyStorage, "Total");
            currencyStorage.ReplaceSessionCurrency(0);
        }
        
        private void SetScoreToUI(MetaContext meta)
        {
            MetaEntity scoreStorage = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionScore,
                MetaMatcher.BestScore
            )).GetSingleEntity();
            
            UpdateCurrentScore((int)scoreStorage.SessionScore, "Score");
            UpdateBestScore((int)scoreStorage.BestScore, "Best");
            if (scoreStorage.SessionScore > scoreStorage.BestScore)
            {
                scoreStorage.ReplaceBestScore(((int)scoreStorage.SessionScore));
            }
            scoreStorage.ReplaceSessionScore(0);
        }
        
        private void UpdateCurrentScore(float score, string scoreType)
        {
            string scoreText = score.ToString();
            currentScore.text = StringUpdater.UpdateString($"{scoreType} {scoreText}");
        }

        private void UpdateBestScore(float score, string scoreType)
        {
            string scoreText = score.ToString();
            maxScore.text = StringUpdater.UpdateString($"{scoreType} {scoreText}");
        }
        
        protected override void Initialize()
        {
            CustomDebug.LogWarning("Dont forget to close windows on GameOver Window");
            _updatableWindowService.CloseAll();
            _model = new GameOverWindowModel(Id, _stateMachine, _staticWindowService, _audioFactory, _sceneLoader);
        }


        protected override void SubscribeUpdates()
        {
            btnReturnHome.onClick.AddListener(_model.ReturnHome);
            btnRestart.onClick.AddListener(_model.Restart);
        }

        protected override void UnsubscribeUpdates()
        {
            btnReturnHome.onClick.RemoveAllListeners();
            btnRestart.onClick.RemoveAllListeners();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _model = null;
        }
    }
}