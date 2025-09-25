using Code.Gameplay.Cameras;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class GrabSystem: IExecuteSystem
    {
        private readonly ITouchInputService _inputService;
        private readonly ICameraProvider _cameraProvider;

        public GrabSystem(
            ITouchInputService inputService,
            ICameraProvider cameraProvider)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
        }

        public void Execute()
        {
            if(!_inputService.GetLeftMouseButtonDown()) return;
            if(_cameraProvider.MainCamera == null) return;
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(_inputService.GetScreenMousePosition());
            if (!UnityEngine.Physics.Raycast(ray, out RaycastHit hit)) return;
            GameEntity tapable = hit.collider.GetComponentInParent<GameEntityBehaviour>().Entity;
            if(tapable.isGrabable == false) return;
            if(tapable.isOnShelf) return;
            tapable.isGrabed = true;
        }
    }
}