using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Code.Input.Service
{
    public class NewInputService :INewInputService
    {
        private Camera _mainCamera;

        public void Init(BaseActions playerInput = null, InputAction moveAction = null, InputAction clickAction = null)
        {
            // Для тача ничего не нужно
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

        /// <summary>
        /// Возвращает позицию первого активного тача на экране
        /// </summary>
        public Vector2 GetScreenTouchPosition()
        {
            if (Touchscreen.current == null || Touchscreen.current.touches.Count == 0)
                return Vector2.zero;

            return Touchscreen.current.touches[0].position.ReadValue();
        }

        /// <summary>
        /// Возвращает позицию тача в мировых координатах
        /// </summary>
        public Vector3 GetWorldTouchPosition()
        {
            if (CameraMain == null)
                return Vector3.zero;

            Vector2 screenPos = GetScreenTouchPosition();
            return CameraMain.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, CameraMain.nearClipPlane));
        }

        /// <summary>
        /// Проверка, был ли тач на этом кадре
        /// </summary>
        public bool GetTouchDown()
        {
            if (Touchscreen.current == null)
                return false;

            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.wasPressedThisFrame)
                {
                    int touchId = touch.touchId.ReadValue();
                    bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touchId);

                    if (!isOverUI)
                    {
                        Debug.Log($"[Input] Touch detected, touchId={touchId}");
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка, удерживается ли тач
        /// </summary>
        public bool GetLeftMouseButton()
        {
            if (Touchscreen.current == null)
                return false;

            foreach (var touch in Touchscreen.current.touches)
            {
                int touchId = touch.touchId.ReadValue();
                bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touchId);

                if (touch.press.isPressed && !isOverUI)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка, отпущен ли тач
        /// </summary>
        public bool GetLeftMouseButtonUp()
        {
            if (Touchscreen.current == null)
                return false;

            foreach (var touch in Touchscreen.current.touches)
            {
                int touchId = touch.touchId.ReadValue();
                bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touchId);

                if (touch.press.wasReleasedThisFrame && !isOverUI)
                    return true;
            }

            return false;
        }

        public bool InputAvaliable { get; set; } = true;
    }
}
