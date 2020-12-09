using UnityEngine;
using System.Collections;

namespace Animations
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float deployDuration;
        [SerializeField] private ParticleSystem shieldParticles;
        [SerializeField] [Range(0, 1)] private float movementSpeed;

        private Animator _animator;
        private Transform _transform;
        private Vector2 _targetPos;
        private static readonly int Opened = Animator.StringToHash("Opened");

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _targetPos = _transform.position;
        }

        private void Update()
        {
            _transform.position = Vector2.Lerp(_transform.position, _targetPos, movementSpeed);
        }

        public void Deploy()
        {
            _animator.SetBool(Opened, true);
            StartCoroutine(WaitAndSetParticles(true, deployDuration));
        }

        public void Retract()
        {
            _animator.SetBool(Opened, false);
            StartCoroutine(WaitAndSetParticles(false, deployDuration));
        }

        public void MoveTo(Vector2 pos)
        {
            _targetPos = pos;
        }

        private IEnumerator WaitAndSetParticles(bool activeOrNot, float waitFor)
        {
            yield return new WaitForSeconds(waitFor);
            
            if (activeOrNot) shieldParticles.Play(); 
            else shieldParticles.Stop(); 
        }
    }
}
