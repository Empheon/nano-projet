using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Ground detection")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector2 groundCastOffset = Vector2.zero;
        [SerializeField] private Vector2 groundCastSize = Vector2.one;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce = 20f;
        [SerializeField] private float accDownForce = 200f;
        [SerializeField] private float maxKeepInAirTime = 0.8f;
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        
        private Gamepad _gamepad;
        private Transform _transform;
        private Rigidbody2D _rb;
        
        private float _movement;
        
        private bool _shouldJump;
        private bool _hasJumped;
        private bool _keepInAir;
        private float _currentTimeJumping;

        private bool IsGrounded
        {
            get
            {
                var hit = Physics2D.BoxCast(
                    _transform.TransformPoint(groundCastOffset), 
                    groundCastSize, 
                    0,
                    Vector2.zero,
                    0,
                    whatIsGround);
                return hit;
            }
        }

        // will be called by CharacterSpawner
        public void OnSpawn(Gamepad gamepad)
        {
            _gamepad = gamepad;
        }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // computed prop is better cached.
            var grounded = IsGrounded;
            
            // updating some jump properties
            if (_hasJumped && !grounded)
            {
                _currentTimeJumping += Time.deltaTime;
            }
            else if(grounded)
            {
                _currentTimeJumping = 0;
                _hasJumped = false;
            }
            
            // input for jump
            if (grounded && _gamepad.buttonSouth.isPressed) _shouldJump = true;
            _keepInAir = _hasJumped && _gamepad.buttonSouth.isPressed;
            
            // interaction
            if (_gamepad.buttonWest.wasPressedThisFrame) Interact();

            _movement = _gamepad.leftStick.x.ReadValue() * moveSpeed;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_movement, _rb.velocity.y);
            
            if (_shouldJump)
            {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _shouldJump = false;
                _hasJumped = true;
            }
            else if (!_keepInAir || _currentTimeJumping > maxKeepInAirTime)
            {
                _rb.AddForce(Vector2.down * (accDownForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }
        }
        
        private void Interact()
        {
            // need to keep track of interactable in range / closest interactable
            // do something with object in range.
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.TransformPoint(groundCastOffset), groundCastSize);
        }
    }
}