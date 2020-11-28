using System;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Character.CharacterController;

namespace NeoMecha.ConsoleControls
{
    public class ConsoleControls : MonoBehaviour
    {
        [SerializeField] private string interactingTag = "Interacting";
        [SerializeField] [Range(0, 1)] private float getOutValueThreshHold = 0.95f;
        [SerializeField] [Range(0, 1)] private float snapValueThreshHold = 0.5f;
        [SerializeField] private float snapCooldown = 0.2f;
        
        private Gamepad _gamepad;
        private CharacterController _controller;
        private ConsoleControlSystem _system;
        private TargetConsole _console;

        private string _originalTag;
        private bool _hasControl = false;
        private float _timeSinceLastSnap = 0f;

        private void Start()
        {
            _system = GetComponent<ConsoleControlSystem>();
            _console = GetComponent<TargetConsole>();

            _originalTag = tag;
        }

        private void OnCharacterInteract(GameObject character)
        {
            if (!_console.CanDoAction()) return;

            _controller = character.GetComponent<CharacterController>();
            _controller.enabled = false;
            
            _gamepad = _controller.GetGamepad();
            _hasControl = true;

            var ok = _system.Activate();
            
            tag = interactingTag;
            
            if (!ok) OnStopInteraction();
        }

        private void OnStopInteraction()
        {
            _controller.enabled = true;
            _controller = null;
            
            _gamepad = null;
            _hasControl = false;
            
            _system.Desactivate();

            tag = _originalTag;
        }

        private void Update()
        {
            _timeSinceLastSnap += Time.deltaTime;
            
            if (_hasControl)
            {
                var x = _gamepad.leftStick.x.ReadValue();
            
                if (x > getOutValueThreshHold || x < -getOutValueThreshHold)
                {
                    OnStopInteraction();
                    return;
                }

                if (_timeSinceLastSnap > snapCooldown)
                {
                    var y = _gamepad.leftStick.y.ReadValue();

                    if (y > snapValueThreshHold)
                    {
                        _system.Previous();
                        _timeSinceLastSnap = 0f;
                    }
                    else if (y < -snapValueThreshHold)
                    {
                        _system.Next();
                        _timeSinceLastSnap = 0f;
                    }
                }

                if (_gamepad.buttonWest.wasPressedThisFrame)
                {
                    _system.Validate();
                    OnStopInteraction();
                }
            }
        }
    }
}