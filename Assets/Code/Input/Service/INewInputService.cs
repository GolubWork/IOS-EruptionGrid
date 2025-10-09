using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Input.Service
{
    public interface INewInputService
    {
        public void Init(BaseActions playerInput = null, InputAction moveAction = null, InputAction clickAction = null);
        Camera CameraMain { get; }
        public bool InputAvaliable { get; set; }

        /// <summary>
        /// Возвращает позицию первого активного тача на экране
        /// </summary>
        public Vector2 GetScreenTouchPosition();

        /// <summary>
        /// Возвращает позицию тача в мировых координатах
        /// </summary>
        public Vector3 GetWorldTouchPosition();

        /// <summary>
        /// Проверка, был ли тач на этом кадре
        /// </summary>
        public bool GetTouchDown();

        /// <summary>
        /// Проверка, удерживается ли тач
        /// </summary>
        public bool GetLeftMouseButton();

        /// <summary>
        /// Проверка, отпущен ли тач
        /// </summary>
        public bool GetLeftMouseButtonUp();
    }
}