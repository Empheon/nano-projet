using System.Collections;
using UnityEngine;

namespace Animations
{
    public class Laser : MonoBehaviour
    {

        [SerializeField] private ParticleSystem targetLockedEffect;
        [SerializeField] private ParticleSystem laserParticles;
        [SerializeField] private Transform laserHandle;

        [SerializeField] private Vector3 offset;
        [SerializeField] [Range(0f, 1f)] private float movementSmoothing;
        [SerializeField] private float targetLockedEffectDuration = 2f;
        
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private float _targetDistance;
        private Vector3 _laserScale;

        public void TurnOn()
        {
            laserParticles.Play();
        }
        
        public void TurnOff()
        {
            laserParticles.Stop();
        }

        public void AimAt(Vector3 point)
        {
            _targetPosition = point;

            var direction = laserHandle.position - point;
            direction = new Vector3(
                direction.x / _laserScale.x,
                direction.y / _laserScale.y,
                direction.z / _laserScale.z
            );
            
            _targetRotation = Quaternion.LookRotation(direction);
            _targetDistance = direction.magnitude;
        }

        public void Shoot()
        {
            targetLockedEffect.transform.position = _targetPosition;
            targetLockedEffect.Play();
            
            StartCoroutine(WaitAndStop(targetLockedEffect, targetLockedEffectDuration));
        }

        private IEnumerator WaitAndStop(ParticleSystem effect, float waitFor)
        {
            yield return new WaitForSeconds(waitFor);
            effect.Stop();
        }

        private void Awake()
        {
            _targetRotation = laserHandle.transform.rotation * Quaternion.Euler(offset);
            _targetDistance = laserParticles.main.startSizeX.constant;
            _laserScale = laserParticles.transform.lossyScale;
        }

        private void Update()
        {
            // lerp rotation
            var correctedAngle = laserHandle.rotation * Quaternion.Euler(offset);
            var nextTargetRotation = Quaternion.Slerp(correctedAngle, _targetRotation, movementSmoothing);

            laserHandle.rotation = nextTargetRotation * Quaternion.Euler(-offset);
            
            // lerp laser lenght
            var main = laserParticles.main;
            var laserLength = main.startSizeX.constant;
            var nextLaserLength = Mathf.Lerp(laserLength, _targetDistance, movementSmoothing);
            
            main.startSizeX = nextLaserLength;
        }
    }
}