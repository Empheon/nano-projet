using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class ZQSDKeyboardAdapter : IGameController
    {
        private bool _ready;
        
        public bool InteractThisFrame() => Keyboard.current.leftShiftKey.wasPressedThisFrame;
        public bool ValidateThisFrame() => Keyboard.current.leftShiftKey.wasPressedThisFrame;
        public bool CancelThisFrame() => false;

        public bool JumpThisFrame() =>
            Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame;

        public bool KeepInAir() =>
            Keyboard.current.wKey.isPressed || Keyboard.current.spaceKey.isPressed;

        public Vector2 GetMovement()
        {
            var baseMove = Vector2.zero;
            if (Keyboard.current.aKey.isPressed) baseMove += Vector2.left;
            if (Keyboard.current.dKey.isPressed) baseMove += Vector2.right;
            if (Keyboard.current.sKey.isPressed) baseMove += Vector2.down;
            if (Keyboard.current.wKey.isPressed) baseMove += Vector2.up;

            return baseMove;
        }

        public void UpdateState()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                _ready = !_ready;
        }

        public bool IsConnected() => IsReady();
        public bool IsReady() => _ready;
    }
}