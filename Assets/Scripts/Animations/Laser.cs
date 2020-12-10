using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class Laser : MonoBehaviour
    {

        [SerializeField] private ParticleSystem targetLockedEffect;
        [SerializeField] private ParticleSystem laserParticles;
        [SerializeField] private ParticleSystem laserFlash;
        [SerializeField] private Transform laserHandle;
        
        [SerializeField] [Range(0f, 1f)] private float movementSmoothing;
        [SerializeField] private float targetLockedEffectDuration = 2f;
        
        private Vector3 _targetPosition;
        private float _targetDistance;

        public void TurnOn()
        {
            AkSoundEngine.PostEvent("ATK_Gun_Shoot_Alarm_Play", gameObject);
            laserParticles.Play();
            laserFlash.Play();
        }
        
        public void TurnOff()
        {
            AkSoundEngine.PostEvent("ATK_Gun_Shoot_Alarm_Stop", gameObject);
            laserParticles.Stop();
            laserFlash.Stop();
        }

        public void AimAt(Vector3 point)
        {
            laserHandle.DOLookAt(point, 0.5f);
            _targetDistance = Vector3.Distance(laserHandle.position, point) / Mathf.Abs(laserParticles.transform.lossyScale.x);
            _targetPosition = point;
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
            _targetDistance = laserParticles.main.startSizeX.constant;
        }

        private void Update()
        {
            // lerp laser lenght
            var main = laserParticles.main;
            var laserLength = main.startSizeX.constant;
            var nextLaserLength = Mathf.Lerp(laserLength, _targetDistance, movementSmoothing);
            
            main.startSizeX = nextLaserLength;
        }
    }
}