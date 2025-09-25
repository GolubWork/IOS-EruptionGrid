using System.Collections.Generic;
using Code.Gameplay.Abilities.Factory;
using Entitas;

namespace Code.Gameplay.Abilities.System
{
    // DotWeen required
    public class CollisionEffectSystem : IExecuteSystem
    {
        private readonly IAbilityFXFactory _abilityFXFactory;
        private readonly IGroup<GameEntity> _abilities;
        private readonly List<GameEntity> _buffer = new(16);
        private readonly IGroup<GameEntity> _cameras;
       // private Tween _tween;
        
        public CollisionEffectSystem(GameContext gameContext, IAbilityFXFactory abilityFXFactory)
        {
            _abilityFXFactory = abilityFXFactory;
            _abilities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Armament,
                    GameMatcher.CollisionEffect,
                    GameMatcher.Reached
                   ));

            _cameras = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Camera,
                    GameMatcher.MainCamera
                ));
            
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            {
                _abilityFXFactory.CreateExplosiveFX(ability.WorldPosition);

                CameraShake();
            }
        }

        private void CameraShake()
        {
            foreach (GameEntity camera in _cameras)
            {
                // if(_tween != null) return;
                // _tween = camera.Transform.DOShakePosition(0.1f, strength: new Vector3(0.2f, 0.2f, 0), vibrato: 3, randomness: 30)
                //     .OnComplete( () => _tween = null);
            }
        }
    }
}