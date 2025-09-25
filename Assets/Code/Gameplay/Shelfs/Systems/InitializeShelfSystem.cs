using Code.Gameplay.Levels;
using Code.Gameplay.Shelfs.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Shelfs.Systems
{
    public class InitializeShelfSystem: IInitializeSystem
    {
        private readonly IShelfFactory _shelfFactory;
        private readonly ILevelDataProvider _levelDataProvider;

        public InitializeShelfSystem(IShelfFactory shelfFactory, ILevelDataProvider levelDataProvider)
        {
            _shelfFactory = shelfFactory;
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _shelfFactory.GetShelf(_levelDataProvider.StartPoint + new Vector3(0, 2.6f, 0));
        }
    }
}