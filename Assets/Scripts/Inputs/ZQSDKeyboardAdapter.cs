using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class ZQSDKeyboardAdapter : IGameController
    {
        private bool _ready;
        
        public bool InteractThisFrame() => Keyboard.current.leftShiftKey.wasPressedThisFrame;

        public bool CancelThisFrame() => false;

        public bool JumpThisFrame() =>
            Keyboard.current.zKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame;

        public Vector2 GetMovement()
        {
            var baseMove = Vector2.zero;
            if (Keyboard.current.qKey.isPressed) baseMove += Vector2.left;
            if (Keyboard.current.dKey.isPressed) baseMove += Vector2.right;
            if (Keyboard.current.sKey.isPressed) baseMove += Vector2.down;

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