using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class GamepadAdapter : IGameController
    {
        public readonly Gamepad Gamepad;
        private bool _ready;
        
        public GamepadAdapter(Gamepad gamepad)
        {
            Gamepad = gamepad;
        }

        public bool InteractThisFrame() => Gamepad.buttonWest.wasPressedThisFrame;
        public bool CancelThisFrame() => Gamepad.buttonEast.wasPressedThisFrame;
        public bool JumpThisFrame() => Gamepad.buttonSouth.wasPressedThisFrame;

        public bool KeepInAir() => Gamepad.buttonSouth.isPressed;

        public Vector2 GetMovement() => Gamepad.leftStick.ReadValue();

        public void UpdateState()
        {
            if (Gamepad.buttonSouth.wasPressedThisFrame)
                _ready = !_ready;
        }

        public bool IsConnected() => true;
        public bool IsReady() => _ready;
    }
}