using Code.Gameplay.Player.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Player.Systems
{
    public class InitializePlayerSystem: IInitializeSystem
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IGroup<GameEntity> _cameras;

        public InitializePlayerSystem(GameContext context, IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _cameras = context.GetGroup(GameMatcher.MainCamera);
        }

        public void Initialize()
        {
            GameEntity player = _playerFactory.CreatePlayer(new Vector3(0, -10, 0));
            _cameras.GetSingleEntity().ReplaceCameraFollowTargetId(player.Id);
        }
    }
}