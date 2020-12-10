using System;
using System.Collections.Generic;
using Inputs;
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
        
        private IGameController _gc;
        private CharacterController _controller;
        private List<ConsoleControlSystem> _systems;
        private TargetConsole _console;

        private string _originalTag;
        private bool _hasControl = false;
        private float _timeSinceLastSnap = 0f;

        private void Start()
        {
            _systems = new List<ConsoleControlSystem>(GetComponents<ConsoleControlSystem>());
            _console = GetComponent<TargetConsole>();

            _originalTag = tag;
        }

        private void OnCharacterInteract(GameObject character)
        {
            if (!_console.CanDoAction()) return;

            _controller = character.GetComponent<CharacterController>();
            _controller.enabled = false;
            _controller.Stop();
            
            _gc = _controller.GetGamepad();
            _hasControl = true;

            bool ok = true;
            foreach (var consoleControlSystem in _systems)
            {
                if (!consoleControlSystem.Activate())
                {
                    ok = false;
                }
            }
            
            tag = interactingTag;
            
            if (!ok) OnStopInteraction();
        }

        private void OnStopInteraction()
        {
            _controller.enabled = true;
            _controller = null;
            
            _gc = null;
            _hasControl = false;

            foreach (var consoleControlSystem in _systems)
            {
                consoleControlSystem.Desactivate();
            }

            tag = _originalTag;
        }

        private void Update()
        {
            _timeSinceLastSnap += Time.deltaTime;
            
            if (_hasControl)
            {
                var x = _gc.GetMovement().x;
            
                if (x > getOutValueThreshHold || x < -getOutValueThreshHold)
                {
                    OnStopInteraction();
                    return;
                }

                if (_timeSinceLastSnap > snapCooldown)
                {
                    var y = _gc.GetMovement().y;

                    if (y > snapValueThreshHold)
                    {
                        foreach (var consoleControlSystem in _systems)
                        {
                            consoleControlSystem.Previous();
                        }
                        _timeSinceLastSnap = 0f;
                    }
                    else if (y < -snapValueThreshHold)
                    {
                        foreach (var consoleControlSystem in _systems)
                        {
                            consoleControlSystem.Next();
                        }
                        _timeSinceLastSnap = 0f;
                    }
                }

                if (_gc.ValidateThisFrame())
                {
                    foreach (var consoleControlSystem in _systems)
                    {
                        consoleControlSystem.Validate();
                    }
                    OnStopInteraction();
                }
            }
        }
    }
}