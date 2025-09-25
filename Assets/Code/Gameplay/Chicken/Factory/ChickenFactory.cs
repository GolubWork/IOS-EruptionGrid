using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.CharacterStats;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.RandomSpriteStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Chicken.Factory
{
    public class ChickenFactory : IChickenFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IRandomSpriteProvider _randomSpriteProvider;

        public ChickenFactory(IIdentifierService identifierService, 
            IVisualEffectStaticDataService visualEffectStaticDataService, 
            IEffectStaticDataService effectStaticDataService,
            IRandomSpriteProvider randomSpriteProvider)
        {
            _identifierService = identifierService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
            _effectStaticDataService = effectStaticDataService;
            _randomSpriteProvider = randomSpriteProvider;
        }

        public GameEntity CreateHero(Vector3 at)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                    .With(x => x[Stats.TapsRequired] = 1)
                ;

            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddViewPath(PrefabsDirectoryConstants.ChickenPrefabPath)
                    .With(e => e.isChicken = true)
                    
                    .With(e => e.isGrabable = true)
                    
                    .AddEggCurrentSpawnTime(2f)
                    .AddEggMaxSpawnTime(2f)
                   // .With(e => e.isEggSpawnActive = true)
                ;
        }
    }
}