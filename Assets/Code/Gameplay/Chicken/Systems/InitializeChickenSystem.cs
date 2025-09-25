using Code.Gameplay.Chicken.Factory;
using Code.Gameplay.Levels;
using Code.Gameplay.StaticData.RandomSpriteStaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Chicken.Systems
{
    public class InitializeChickenSystem : IInitializeSystem
    {
        private readonly IChickenFactory _chickenFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IRandomSpriteProvider _randomSpriteProvider;

        public InitializeChickenSystem(IChickenFactory chickenFactory, ILevelDataProvider levelDataProvider, IRandomSpriteProvider randomSpriteProvider)
        {
            _chickenFactory = chickenFactory;
            _levelDataProvider = levelDataProvider;
            _randomSpriteProvider = randomSpriteProvider;
        }

        public void Initialize()
        {
            _chickenFactory.CreateHero(_levelDataProvider.StartPoint + new Vector3(0, 3f, 0));
        }
    }
}