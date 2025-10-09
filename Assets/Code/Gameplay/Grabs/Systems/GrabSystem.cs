using Code.Gameplay.Cameras;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Grabs.Systems
{
    public class GrabSystem: IExecuteSystem
    {
        private readonly INewInputService _inputService1;
        private readonly ICameraProvider _cameraProvider;

        public GrabSystem(
            INewInputService inputService1,
            ICameraProvider cameraProvider)
        {
            _inputService1 = inputService1;
            _cameraProvider = cameraProvider;
        }

        public void Execute()
        {
            if(!_inputService1.GetTouchDown()) return;
            if(_cameraProvider.MainCamera == null) return;
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(_inputService1.GetScreenTouchPosition());
            if (!UnityEngine.Physics.Raycast(ray, out RaycastHit hit)) return;
            GameEntity tapable = hit.collider.GetComponentInParent<GameEntityBehaviour>().Entity;
            if(tapable.isGrabable == false) return;
            if(tapable.isOnShelf) return;
            tapable.isGrabed = true;
        }
    }
}