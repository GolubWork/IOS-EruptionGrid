using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Code.Input.Service
{
    public class NewInputService : INewInputService
    {
        private Camera _mainCamera;

        private Vector2 _swipeStartPos;
        private Vector2 _swipeEndPos;
        private bool _isSwiping;
        private bool _swipeDetected;
        private Vector3 _swipeDirection;

        private const float SwipeThreshold = 50f;
        
        public bool InputAvaliable { get; set; } = true;

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

        // ======== TOUCH HELPERS ========

        bool INewInputService.SwipeDetected() => _swipeDetected;
        Vector3 INewInputService.SwipeDirection() => _swipeDirection;

        public Vector2 GetScreenTouchPosition()
        {
            if (Touchscreen.current == null || Touchscreen.current.touches.Count == 0)
                return Vector2.zero;

            return Touchscreen.current.touches[0].position.ReadValue();
        }

        public Vector3 GetWorldTouchPosition()
        {
            if (CameraMain == null)
                return Vector3.zero;

            Vector2 screenPos = GetScreenTouchPosition();
            return CameraMain.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, CameraMain.nearClipPlane));
        }

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
                        return true;
                }
            }

            return false;
        }

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

        // ======== SWIPE LOGIC ========

        public void UpdateSwipe()
        {
            _swipeDetected = false;

            // начало свайпа
            if (GetTouchDown())
            {
                _swipeStartPos = GetScreenTouchPosition();
                _isSwiping = true;
            }

            // конец свайпа
            if (_isSwiping && GetLeftMouseButtonUp())
            {
                _isSwiping = false;
                _swipeEndPos = GetScreenTouchPosition();

                Vector2 delta = _swipeEndPos - _swipeStartPos;

                if (delta.magnitude < SwipeThreshold)
                    return;

                _swipeDirection = CalculateSwipeDirection(delta);
                _swipeDetected = true;
            }
        }

        private Vector3 CalculateSwipeDirection(Vector2 delta)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                return delta.x > 0 ? Vector3.right : Vector3.left;
            }
            else
            {
                return delta.y > 0 ? Vector3.up : Vector3.down;
            }
        }
    }
}
