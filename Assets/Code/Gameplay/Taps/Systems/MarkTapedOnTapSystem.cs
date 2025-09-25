using Code.Common.Helpers;
using Code.Gameplay.Cameras;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Taps.Systems
{
    public class MarkTapedOnTapSystem: IExecuteSystem
    {
        private readonly ITouchInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGroup<InputEntity> _inputs;

        public MarkTapedOnTapSystem(
            ITouchInputService inputService,
            ICameraProvider cameraProvider,
            InputContext input)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _inputs = input.GetGroup(InputMatcher.AllOf(
                InputMatcher.Input
                ));
        }

        public void Execute()
        {
            if(!_inputs.GetSingleEntity().isInputAvaliable) return;
            if(!_inputService.GetLeftMouseButtonDown()) return;
            if(_cameraProvider.MainCamera == null) return;
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(_inputService.GetScreenMousePosition());
            if (!UnityEngine.Physics.Raycast(ray, out RaycastHit hit)) return;
            GameEntity tapable = hit.collider.GetComponentInParent<GameEntityBehaviour>().Entity;
            if(tapable.isTapable == false) return;
            if(tapable.hasTapsRequired == false) return;
            tapable.ReplaceTapsRequired(tapable.TapsRequired - 1);
            tapable.isTaped = true;
        }
    }
}