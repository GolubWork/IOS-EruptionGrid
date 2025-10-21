using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Configs;
using Code.Gameplay.Effects.Factory;
using Code.Gameplay.StaticData.EffectStaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class OneStepMovementSystem : IExecuteSystem
    {
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IEffectFactory _effectFactory;
        private readonly IAudioFactory _audioFactory;
        private readonly IGroup<GameEntity> _movers;
        private readonly IGroup<GameEntity> _trees;
        private float savedY = -15;

        public OneStepMovementSystem(GameContext game, IEffectStaticDataService effectStaticDataService, IEffectFactory effectFactory, IAudioFactory audioFactory)
        {
            _effectStaticDataService = effectStaticDataService;
            _effectFactory = effectFactory;
            _audioFactory = audioFactory;
            _movers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldPosition,
                GameMatcher.Direction,
                GameMatcher.Transform,
                GameMatcher.OneStepMovement,
                GameMatcher.OneStepMovementBoundsX,
                GameMatcher.OneStepMovementBoundsY
            ));
            
            _trees = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TreeObstacle,
                GameMatcher.WorldPosition
            ));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                if (mover.Direction == Vector2.zero)
                    continue;

                Vector3 currentPos = mover.WorldPosition;
                Vector3 newPos = currentPos + (Vector3)mover.Direction;

                Vector2Int boundsX = mover.OneStepMovementBoundsX;
                Vector2Int boundsY = mover.OneStepMovementBoundsY;

                newPos.x = Mathf.Clamp(newPos.x, boundsX.x, boundsX.y);
                newPos.y = Mathf.Max(newPos.y, boundsY.x);
                
                bool blocked = false;
                Vector3 targetPos = currentPos + (Vector3)mover.Direction;
                foreach (GameEntity tree in _trees)
                {
                    if (ApproximatelyEqual(tree.WorldPosition, targetPos))
                    {
                        blocked = true;
                        break;
                    }
                }

                bool bordersX = targetPos.x < -3 | targetPos.x > 3;
                bool maxBack = targetPos.y < savedY - 5;
                bool startCheck = targetPos.y < -10;
                if (bordersX || maxBack || startCheck)
                    blocked = true;
                
              
                if (!blocked)
                {
                    Vector3 flatDirection = new Vector3(mover.Direction.x, mover.Direction.y, 0);
                    if (flatDirection.sqrMagnitude > 0.001f)
                    {
                        float turnAngle = Mathf.Atan2(flatDirection.z, flatDirection.x) * Mathf.Rad2Deg;
                        mover.Transform.rotation = Quaternion.Euler(0, turnAngle, 0);
                    }

                    if (targetPos.y > savedY)
                    {
                        savedY = targetPos.y;
                        EffectConfig effectConfig = _effectStaticDataService.GetEffectConfig(EffectTypeId.AddPoints);
                        _effectFactory.CreateEffect(effectConfig, mover.Id, mover.Id);
                    }
                    _audioFactory.CreateSound(SoundTypeId.Swipe);
                    mover.ReplaceWorldPosition(targetPos);
                }

                mover.ReplaceDirection(Vector2.zero);
            }
        }
        private bool ApproximatelyEqual(Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }
    }
}