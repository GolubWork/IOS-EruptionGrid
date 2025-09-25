using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Common.Helpers;
using Code.Input.Service;
using Entitas;
using UnityEngine.InputSystem;

namespace Code.Input.Systems
{
    public class InitializeInputSystem : IInitializeSystem, ITearDownSystem
    {
        private readonly ITouchInputService _inputService;
        private BaseActions _actions;
        private readonly IGroup<InputEntity> _inputs;

        public InitializeInputSystem(InputContext input, ITouchInputService inputService)
        {
            _inputService = inputService;
            _inputs = input.GetGroup(InputMatcher.Input);
        }

        public void Initialize()
        {
            _actions = new BaseActions();
            _actions.Enable();

            InputAction moveAction = _actions.Gameplay.Slide;
            InputAction clickAction = _actions.Gameplay.Tap;
            
            CustomDebug.Log($"Initializing Input System, moveAction: {moveAction}, clickAction: {clickAction}");
            
            _inputService.Init(_actions, moveAction, clickAction);
            
            if(_inputs.GetEntities().Length > 0) return;
            CreateInputEntity.Empty()
                .With(e => e.isInput = true)
                .With(e => e.isInputAvaliable = true)
                ;
        }

        public void TearDown()
        {
            if(_actions == null) return;
            _actions.Disable();
        }
    }
}