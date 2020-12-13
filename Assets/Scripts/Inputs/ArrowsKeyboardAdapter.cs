using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class ArrowsKeyboardAdapter : IGameController
    {
        private bool _ready;
        
        public bool InteractThisFrame() => Keyboard.current.rightCtrlKey.wasPressedThisFrame;

        public bool ValidateThisFrame() => Keyboard.current.rightCtrlKey.wasPressedThisFrame;

        public bool CancelThisFrame() => false;

        public bool JumpThisFrame() => Keyboard.current.upArrowKey.wasPressedThisFrame;

        public bool KeepInAir() => Keyboard.current.upArrowKey.isPressed;

        public Vector2 GetMovement()
        {
            var baseMove = Vector2.zero;
            if (Keyboard.current.leftArrowKey.isPressed) baseMove += Vector2.left;
            if (Keyboard.current.rightArrowKey.isPressed) baseMove += Vector2.right;
            if (Keyboard.current.downArrowKey.isPressed) baseMove += Vector2.down;
            if (Keyboard.current.upArrowKey.isPressed) baseMove += Vector2.up;

            return baseMove;
        }

        public bool PauseThisFrame() => Keyboard.current.backspaceKey.wasPressedThisFrame;

        public void UpdateState()
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                _ready = !_ready;
        }

        public bool IsConnected() => IsReady();
        public bool IsReady() => _ready;
    }
}