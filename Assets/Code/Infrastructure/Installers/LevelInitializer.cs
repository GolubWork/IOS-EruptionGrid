using Code.Gameplay.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform startPoint;
        private ILevelDataProvider _levelDataProvider;

        [Inject]
        private void Construct(
            ILevelDataProvider levelDataProvider
        )
        {
            _levelDataProvider = levelDataProvider;
        }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _levelDataProvider.SetStartPoint(startPoint.position);
        }
    }
}