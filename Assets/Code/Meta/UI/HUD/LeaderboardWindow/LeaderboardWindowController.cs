using System.Collections.Generic;
using System.Linq;
using Code.Audios.Audio.Factory;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.LeaderboardWindow
{
    public class LeaderboardWindowController: StaticWindow
    {
        [SerializeField] private RectTransform contriner;
        [SerializeField] private LeaderBoardBar _prefab;
        [SerializeField] private Button btnReturn;
        
        private LeaderboardWindowModel _model;
        private IStaticWindowService _staticWindowService;
        private float _score;
        private MetaContext _meta;
        private IAudioFactory _audioFactory;

        [Inject]
        private void Construct(IStaticWindowService staticWindowService, IAudioFactory audioFactory, MetaContext meta)
        {
            Id = StaticWindowId.LeaderBoardWindow;
            _meta = meta;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
        }
        protected override void Initialize()
        {
            _model = new LeaderboardWindowModel(_staticWindowService, _audioFactory);
            CreatePlayerBar();
        }

        protected override void SubscribeUpdates()
        {
            btnReturn.onClick.AddListener(_model.ReturnHome);
        }

        protected override void UnsubscribeUpdates()
        {
            btnReturn.onClick.RemoveListener(_model.ReturnHome);
        }

        private void CreatePlayerBar()
        {
            _score = _meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.BestScore,
                MetaMatcher.Storage
            )).GetSingleEntity().BestScore;
            
            
            List<(string name, int score, bool isPlayer)> players = new List<(string, int, bool)>
            {
                ("Alice", 2500, false),
                ("Bob", 2300, false),
                ("Charlie", 2200, false),
                ("Diana", 2100, false),
                ("Eve", 2000, false),
                ("Frank", 1800, false),
                ("Grace", 1700, false),
                ("Heidi", 1600, false),
                ("Ivan", 1500, false),
                ("Judy", 1400, false),
                ("Karl", 1300, false),
                ("Leo", 1200, false),
                ("Mallory", 1100, false),
                ("Niaj", 1000, false),
                ("Olivia", 900, false),
                ("Peggy", 800, false),
                ("You", (int)_score, true) 
            };
            
            players = players.OrderByDescending(p => p.score).ToList();
            for (int i = 0; i < players.Count; i++)
            {
                LeaderBoardBar bar = Instantiate(_prefab, contriner);
                bar.Setup(players[i].name, players[i].score, i + 1);
                if (players[i].isPlayer)
                {
                    bar.Highlight();
                }
            }
        }
        
        protected override void Cleanup()
        {
            _meta = null;
            _staticWindowService.Close(Id);
        }
    }
}