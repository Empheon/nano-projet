using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class GamepadAdapter : IGameController
    {
        private readonly Gamepad _gamepad;
        private bool _ready;
        
        public GamepadAdapter(Gamepad gamepad)
        {
            _gamepad = gamepad;
        }

        public bool InteractThisFrame() => _gamepad.buttonWest.wasPressedThisFrame;
        public bool CancelThisFrame() => _gamepad.buttonEast.wasPressedThisFrame;
        public bool JumpThisFrame() => _gamepad.buttonSouth.wasPressedThisFrame;
        public Vector2 GetMovement() => _gamepad.leftStick.ReadValue();

        public void UpdateState()
        {
            if (_gamepad.buttonSouth.wasPressedThisFrame)
                _ready = !_ready;
        }

        public bool IsConnected() => true;
        public bool IsReady() => _ready;
    }
}