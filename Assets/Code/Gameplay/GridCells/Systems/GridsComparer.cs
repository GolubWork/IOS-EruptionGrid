using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;

namespace Code.Gameplay.Grids.Systems
{
    public class GridsComparer: IExecuteSystem
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IAudioFactory _audioFactory;
        private readonly IGroup<GameEntity> _playerGrid;
        private readonly IGroup<GameEntity> _referenceGrid;
        private bool isWin;
        private readonly IGroup<MetaEntity> _currency;

        public GridsComparer(GameContext game, IGameStateMachine gameStateMachine,
            IAudioFactory audioFactory, MetaContext meta)
        {
            _gameStateMachine = gameStateMachine;
            _audioFactory = audioFactory;
            _playerGrid = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.PlayerGrid,
                GameMatcher.GridRows
            ));

            _referenceGrid = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.ReferenceGrid,
                GameMatcher.GridRows
            ));
            
            _currency = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionCurrency
            ));
        }

        public void Execute()
        {
            if (isWin) return;
            foreach (GameEntity grid in _playerGrid)
            foreach (GameEntity referenceGrid in _referenceGrid)
            {
                if (grid.GridRows.IsEqual(referenceGrid.GridRows))
                {
                    _currency.GetSingleEntity().ReplaceSessionCurrency(_currency.GetSingleEntity().SessionCurrency + 25);
                    _audioFactory.CreateSound(SoundTypeId.Win);
                    _gameStateMachine.Enter<GameWinState>();
                    isWin = true;
                }
            }
        }
    }
}