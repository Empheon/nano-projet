using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(
        typeof(Rigidbody2D), 
        typeof(CharacterController), 
        typeof(CharacterInteractor))
    ]
    public class CharacterLadderController : MonoBehaviour
    {
        [Header("Climbing")]
        [SerializeField] private float climbSpeed = 3f;

        [Header("Eject (Jump out)")] 
        [SerializeField] private float ejectForce = 200f;
        [SerializeField] [Range(0f, 1f)] private float ejectUpPower = 0.5f; 
        
        private Gamepad _gamepad;
        private Transform _transform;
        private Rigidbody2D _rb;
        private CharacterController _controller;
        private CharacterInteractor _interactor;
        
        private float _movement;
        
        private void OnSpawn(Gamepad gamepad)
        {
            _gamepad = gamepad;
        }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
            
            _controller = GetComponent<CharacterController>();
            _interactor = GetComponent<CharacterInteractor>();
            
            // make sure it is disabled by default
            enabled = false;
        }

        private void Update()
        {
            _movement = _gamepad.leftStick.y.ReadValue() * climbSpeed;

            if (_gamepad.buttonWest.wasPressedThisFrame) StopClimbing();
            if (_gamepad.buttonSouth.wasPressedThisFrame) StopClimbing(true);
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.up * _movement;
        }

        public void StartClimbing(Vector2 from)
        {
            _rb.isKinematic = true;
            _rb.MovePosition(from);
            
            _controller.enabled = false;
            _interactor.enabled = false;
            
            enabled = true;
        }

        public void StopClimbing(bool shouldEject = false)
        {
            _rb.isKinematic = false;
            
            if (shouldEject)
            {
                var ejectDirection = new Vector2(_gamepad.leftStick.x.ReadValue(), ejectUpPower);
                ejectDirection.Normalize();
                
                _rb.AddForce(ejectDirection * ejectForce, ForceMode2D.Impulse);
            }
            
            _controller.enabled = true;
            _interactor.enabled = true;
            
            enabled = false;
        }
    }
}