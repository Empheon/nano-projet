using System.Collections;
using Inputs;
using UnityEngine;

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

        [Header("Crouch")] 
        [SerializeField] [Range(0f, -1f)] private float stickCrouchThreshold = -0.5f;
        [SerializeField] private float ignoreCollisionTime = 1.5f;
        [SerializeField] private string oneWayPlatformTag = "One-way Platform";
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] [Range(0f, 0.6f)] private float deadZone = 0.15f;

        [Header("Sprites & Animations")] 
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float fallingThreshold = -0.05f;
        [SerializeField] private float blinkFrequency = 0.3f;
        [SerializeField] private float scratchFrequency = 0.3f;
        
        private IGameController _gc;
        private Transform _transform;
        private BoxCollider2D _col;
        private Rigidbody2D _rb;
        
        private float _movement;
        
        private bool _shouldJump;
        private bool _hasJumped;
        private bool _keepInAir;
        private float _currentTimeJumping;

        public IGameController GetGamepad()
        {
            return _gc;
        }

        /**
         * Reset the controller's movement
         * /!\ does not disable the controller 
         */
        public void Stop()
        {
            _rb.velocity = Vector2.zero;
            _movement = 0;
        }
        
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
        private void OnSpawn(IGameController gc)
        {
            _gc = gc;
        }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<BoxCollider2D>();
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

            if (_gc.GetMovement().y < stickCrouchThreshold)
            {
                var found = Physics2D.OverlapBox(_transform.TransformPoint(groundCastOffset), groundCastSize, 0, whatIsGround);
                
                if (found && found.CompareTag(oneWayPlatformTag))
                {
                    StartCoroutine(FallThroughPlatform(found));
                }
            }

            if (_gc.JumpThisFrame() && grounded)
            {
                _shouldJump = true;
                animator.SetTrigger("Jump");
                AkSoundEngine.PostEvent("Jump_Character", gameObject);
            }
            
            _keepInAir = _gc.KeepInAir();

            // input for movement
            _movement = _gc.GetMovement().x;
            
            // dead zone
            if (_movement < deadZone && _movement > -deadZone) _movement = 0;

            // i am speed
            _movement *= moveSpeed;
            
            // animations update
            // need to not change if equals to 0
            if (_movement > 0) spriteRenderer.flipX = true;
            else if (_movement < 0) spriteRenderer.flipX = false;

            animator.SetBool("IsRunning", Mathf.Abs(_movement) > 0f);
            animator.SetBool("IsFalling", !grounded && _rb.velocity.y < fallingThreshold);
            animator.SetBool("IsJumping", _hasJumped);

            if(Random.Range(0f, 1f) < blinkFrequency * Time.deltaTime) animator.SetTrigger("Blink");
            if(Random.Range(0f, 1f) < scratchFrequency * Time.deltaTime) animator.SetTrigger("Scratch");
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

        private IEnumerator FallThroughPlatform(Collider2D platformCollider)
        {
            Physics2D.IgnoreCollision(platformCollider, _col, true);
            yield return new WaitForSeconds(ignoreCollisionTime);
            Physics2D.IgnoreCollision(platformCollider, _col, false);
        }

#if UNITY_EDITOR
            
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.TransformPoint(groundCastOffset), groundCastSize);
        }
        
#endif
    }
}