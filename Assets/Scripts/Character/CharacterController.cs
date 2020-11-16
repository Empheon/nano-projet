using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Ground detection")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector2 groundCastOffset = Vector2.zero;
        [SerializeField] private Vector2 groundCastSize = Vector2.one;
        [SerializeField] private float timeBeforeGroundCheck = 0.1f;
        
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

        private bool IsGrounded()
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

        // message is send by CharacterSpawner
        private void OnSpawn(Gamepad gamepad)
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
            bool grounded;

            // skip check for the beginning of the jump
            if (_hasJumped && _currentTimeJumping < timeBeforeGroundCheck) grounded = false;
            else grounded = IsGrounded();

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
            if (grounded && _gamepad.buttonSouth.wasPressedThisFrame) _shouldJump = true;
            _keepInAir = _gamepad.buttonSouth.isPressed;

            // input for movement
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
            
            // accelerate falling when not maintaining jump action (or after max time)
            if (!_keepInAir || _currentTimeJumping > maxKeepInAirTime)
            {
                _rb.AddForce(Vector2.down * (accDownForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.TransformPoint(groundCastOffset), groundCastSize);
        }
    }
}