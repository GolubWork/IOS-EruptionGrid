using System;
using Code.Audios.Audio.Factory;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.EffectsVisual.Factories
{
    public class VisualEffectFactory : IVisualEffectFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IAudioFactory _audioFactory;

        public VisualEffectFactory(IIdentifierService identifierService, IAudioFactory audioFactory)
        {
            _identifierService = identifierService;
            _audioFactory = audioFactory;
        }

        public GameEntity CreateVisualEffect(VisualEffectConfig config, int producerId, int targetId, Vector3 at)
        {
            _audioFactory.CreateSound(config.effectSound);
            switch (config.effectTypeId)
            {
                case VisualEffectTypeId.TapEffect:
                {
                    return CreateTapEffect(producerId, targetId, config, at);
                }
                case VisualEffectTypeId.Collect:
                {
                    return CreateCollectEggEffect(producerId, targetId, config, at);
                }
            }
            throw new Exception($"Effect with type id {config.effectTypeId} does not exist");
        }
        
        
        private GameEntity CreateTapEffect(int producerId, int targetId, VisualEffectConfig config, Vector3 at)
        {
            
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .With(x => x.isVisualEffect = true)
                    .With(x => x.isTapVisualEffect = true)
                    .AddWorldPosition(at)
                    .AddViewPrefab(config.prefab)
                    .AddVisualEffectProducerId(producerId)
                    .AddVisualEffectTargetId(targetId)
                    .AddVisualEffectConfig(config)
                    .AddSelfDestructTimer(config.selfDestructTimer)
                ;
        }
        private GameEntity CreateCollectEggEffect(int producerId, int targetId, VisualEffectConfig config, Vector3 at)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .With(x => x.isVisualEffect = true)
                    .With(x => x.isCollectEggVisualEffect = true)
                    .AddWorldPosition(at)
                    .AddViewPrefab(config.prefab)
                    .AddVisualEffectProducerId(producerId)
                    .AddVisualEffectTargetId(targetId)
                    .AddVisualEffectConfig(config)
                    .AddSelfDestructTimer(config.selfDestructTimer)
                ;
        }
    }
}