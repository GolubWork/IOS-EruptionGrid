using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Input.Service
{
    public interface ITouchInputService
    {
        public void Init(BaseActions playerInput, InputAction moveAction, InputAction clickAction);
        bool HasTouchInput();
        float GetVerticalAxis();
        float GetHorizontalAxis();
        bool HasAxisInput();
    
        bool GetLeftMouseButtonDown();
        Vector2 GetScreenMousePosition();
        Vector2 GetWorldMousePosition();
        bool GetLeftMouseButtonUp();

        bool InputAvaliable { get; set; }
    }
}