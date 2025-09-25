using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Abilities;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.StaticData.AbilityStaticData;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Infrastructure.Identifiers;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Gameplay.Armaments.Factory
{
    public class ArmamentFactory : IArmamentFactory
    {
        private const int TargetBufferSize = 16;

        private readonly IIdentifierService _identifiers;
        private readonly IAbilityStaticDataService _staticDataService;

        public ArmamentFactory(IIdentifierService identifiers, IAbilityStaticDataService staticDataService)
        {
            _identifiers = identifiers;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateMeteor(int level, Vector3 at)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.Meteor, level);
            ProjectileSetup setup = abilityLevel.ProjectileSetup;

            return CreateProjectileEntity(at, abilityLevel, setup)
                .AddParentAbility(AbilityId.Meteor)
                .AddRotationSpeed(Random.Range(-90, 90))
                .With(x => x.isRotationRandomDirection = true);
        }

        private GameEntity CreateProjectileEntity(Vector3 at, AbilityLevel abilityLevel, ProjectileSetup setup)
        {
            GameEntityBehaviour viewPrefab = GetPrefab(abilityLevel);
            
            return CreateGameEntity.Empty()
                    .AddId(_identifiers.Next())
                    .With(x => x.isArmament = true)
                    .AddViewPrefab(viewPrefab)
                    .AddWorldPosition(at)
                    .AddSpeed(setup.Speed)
                    .With(x => x.AddEffectSetups(abilityLevel.EffectSetups),
                        when: !abilityLevel.EffectSetups.IsNullOrEmpty())
                    .With(x => x.AddStatusSetups(abilityLevel.StatusSetups),
                        when: !abilityLevel.StatusSetups.IsNullOrEmpty())
                    .With(x => x.AddTargetLimit(setup.Pierce), when: setup.Pierce > 0)
                    .With(x => x.AddCollisionEffect(abilityLevel.CollisionEffectPrefab),
                        when: !abilityLevel.CollisionEffectPrefab.IsUnityNull())
                    .AddRadius(setup.ContactRadius)
                    .AddTargetsBuffer(new List<int>(TargetBufferSize))
                    .AddProcessedTargets(new List<int>(TargetBufferSize))
                    .AddLayerMask(CollisionLayer.Hero.AsMask())
                    .With(x => x.isMovementAvailable = true)
                    .With(x => x.isReadyToCollectTargets = true)
                    .With(x => x.isCollectingTargetsContinuously = true)
                    .With(x => x.isMoveWithNoBounds = true)
                    .AddSelfDestructTimer(setup.Lifetime)
                ;
        }

        private GameEntityBehaviour GetPrefab(AbilityLevel abilityLevel)
        {
            return abilityLevel.ViewPrefab;
        }
    }
}