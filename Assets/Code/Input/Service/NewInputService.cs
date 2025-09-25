using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Code.Input.Service
{
    public class NewInputService : ITouchInputService
    {
        private Camera _mainCamera;
        private Vector3 _screenPosition;

        private BaseActions _playerInput;
        private InputAction _moveAction;
        private InputAction _clickAction;

        public void Init(BaseActions playerInput, InputAction moveAction, InputAction clickAction)
        {
            _playerInput = playerInput;
            _moveAction = moveAction;
            _clickAction = clickAction;
        }
        public Camera CameraMain
        {
            get
            {
                if (_mainCamera == null && Camera.main != null)
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
        }

        public Vector2 GetScreenMousePosition()
        {
            if (Mouse.current != null)
                return Mouse.current.position.ReadValue();
            return Vector2.zero;
        }

        public Vector2 GetWorldMousePosition()
        {
            if (CameraMain == null)
                return Vector2.zero;

            _screenPosition.x = GetScreenMousePosition().x;
            _screenPosition.y = GetScreenMousePosition().y;
            return CameraMain.ScreenToWorldPoint(_screenPosition);
        }

        public bool HasAxisInput()
        {
            Vector2 move = _moveAction.ReadValue<Vector2>();
            return move != Vector2.zero;
        }

        public bool HasTouchInput()
        {
            return Touchscreen.current != null && Touchscreen.current.touches.Count > 0;
        }

        public float GetVerticalAxis()
        {
            return _moveAction.ReadValue<Vector2>().y;
        }

        public float GetHorizontalAxis()
        {
            return _moveAction.ReadValue<Vector2>().x;
        }

        public bool GetLeftMouseButton()
        {
            return _clickAction.ReadValue<float>() > 0 && !EventSystem.current.IsPointerOverGameObject();
        }

        public bool GetLeftMouseButtonDown()
        {
            return _clickAction.WasPressedThisFrame() && !EventSystem.current.IsPointerOverGameObject();
        }

        public bool GetLeftMouseButtonUp()
        {
            return _clickAction.WasReleasedThisFrame() && !EventSystem.current.IsPointerOverGameObject();
        }

        public bool InputAvaliable { get; set; } = true;
    }
}
