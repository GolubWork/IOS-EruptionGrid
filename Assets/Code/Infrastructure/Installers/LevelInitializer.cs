using Code.Gameplay.Levels;
using Code.Infrastructure.DependencyInjection;
using UnityEngine;

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
        public void Initialize()
        {
            _levelDataProvider.SetStartPoint(startPoint.position);
        }
    }
}