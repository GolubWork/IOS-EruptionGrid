using Code.Common.Extensions;
using Code.Common.Helpers;
using Code.Gameplay.Buildings.Configs;
using Code.Gameplay.Buildings.Factories;
using Code.Gameplay.Effects;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.Levels;
using Code.Gameplay.StaticData.BuildingStaticData;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Buildings.Systems
{
    public class InitializeBuildingSystem: IInitializeSystem
    {
        private readonly IBuildingStaticDataProvider _buildingStaticDataProvider;
        private readonly IBuildingFactory _buildingFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IGroup<MetaEntity> _leveldatas;

        public InitializeBuildingSystem(MetaContext meta,
            IBuildingStaticDataProvider buildingStaticDataProvider, 
            IBuildingFactory buildingFactory,
            ILevelDataProvider levelDataProvider, 
            IEffectStaticDataService effectStaticDataService,
            IVisualEffectStaticDataService visualEffectStaticDataService)
        {
            _buildingStaticDataProvider = buildingStaticDataProvider;
            _buildingFactory = buildingFactory;
            _levelDataProvider = levelDataProvider;
            _effectStaticDataService = effectStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;

            _leveldatas = meta.GetGroup(MetaMatcher.ChosenLevel);
        }

        public void Initialize()
        {
            foreach (BuildingConfig building in _buildingStaticDataProvider.GetConfig().Buildings)
            {
                if(building.TypeId == BuildingTypeId.Unknown) continue;
                CreateBuilding(building);
            }
        }

        private void CreateBuilding(BuildingConfig building)
        {
            switch (building.TypeId)
            {
                case BuildingTypeId.Barracks:
                {
                    CustomDebug.LogWarning("Need to asign spawn point");
                    GameEntity barraks = _buildingFactory.Create(Vector3.zero, BuildingTypeId.Barracks);
                    
                    barraks.AddTotalTapsRequired(1)
                        .AddTapsRequired(1)
                        .AddTapRapeatableTimes(-1)
                        .AddTapEffectConfig(
                            _effectStaticDataService.GetEffectConfig(EffectTypeId.Tap))
                        .AddTapVisualEffectConfig(
                            _visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.TapEffect))
                        .With(e => e.isTapable = true);
                    break;
                }
                case BuildingTypeId.Pyramid:
                {
                    CustomDebug.LogWarning("Need to asign spawn point");
                    GameEntity pyramid = _buildingFactory.Create(Vector3.zero, BuildingTypeId.Pyramid);
                    CustomDebug.LogWarning("Need to asign PyramidResourceRequirement");
                    pyramid.ReplaceMaxBuildingStorage(1);
                    break;
                }
                case BuildingTypeId.Bank:
                {
                    CustomDebug.LogWarning("Need to asign spawn point");
                    _buildingFactory.Create(Vector3.zero, BuildingTypeId.Bank);
                    break;
                }
                case BuildingTypeId.Relics:
                {
                    CustomDebug.LogWarning("Need to asign spawn point");
                    _buildingFactory.Create(Vector3.zero, BuildingTypeId.Relics);
                    break;
                }
            }
        }
    }
}