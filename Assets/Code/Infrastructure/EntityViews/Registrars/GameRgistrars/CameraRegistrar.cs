using Code.Gameplay.Cameras;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.EntityViews.Registrars.GameRgistrars
{
    public sealed class CameraRegistrar : AutoComponentRegistrar<GameEntityBehaviour, GameEntity, Camera>
    {
        private ICameraProvider _cameraProvider;

        [Inject]
        private void Construct(ICameraProvider cameraProvider) => _cameraProvider = cameraProvider;
    
        public override void RegisterComponents()
        {
            base.RegisterComponents();
            _cameraProvider.SetMainCamera(Component);
        }

        public override void UnRegisterComponents()
        {
            base.RegisterComponents();
            _cameraProvider.SetMainCamera(null);
        }
    }
}