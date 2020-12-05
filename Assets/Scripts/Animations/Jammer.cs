using System;
using System.Collections;
using UnityEngine;

namespace Animations
{
    public class Jammer : MonoBehaviour
    {
        [SerializeField] private Transform jammerHandle;
        
        [SerializeField] private ParticleSystem teslaCoil;
        [SerializeField] private ParticleSystem electricBolt;
        [SerializeField] private ParticleSystem electricBoltBlast;
        [SerializeField] private ParticleSystem impactBlast;
        
        [SerializeField] private Vector3 offset;
        [SerializeField] [Range(0f, 1f)] private float movementSmoothing;
        
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private float _targetDistance;
        
        public void TurnOn()
        {
            teslaCoil.Play();
            electricBolt.Play();
        }
        
        public void TurnOff()
        {
            teslaCoil.Stop();
            electricBolt.Stop();
        }

        public void AimAt(Vector3 point)
        {
            var laserOrigin = jammerHandle.position;

            _targetPosition = point;
            _targetRotation = Quaternion.LookRotation(laserOrigin - point);
            _targetDistance = Vector3.Distance(laserOrigin, point);
        }

        public void Blast()
        {
            electricBoltBlast.Play();

            impactBlast.transform.position = _targetPosition;
            impactBlast.Play();
        }
        
        private void Awake()
        {
            _targetRotation = jammerHandle.transform.rotation * Quaternion.Euler(offset);
            _targetDistance = electricBolt.main.startSizeX.constant;
        }

        private void Update()
        {
            // lerp rotation
            var correctedAngle = jammerHandle.rotation * Quaternion.Euler(offset);
            var nextTargetRotation = Quaternion.Slerp(correctedAngle, _targetRotation, movementSmoothing);

            jammerHandle.rotation = nextTargetRotation * Quaternion.Euler(-offset);
            
            // lerp laser lenght
            var boltMain = electricBolt.main;
            var blastMain = electricBoltBlast.main;
            var boltLength = boltMain.startSizeY.constant;
            var nextBoltLength = Mathf.Lerp(boltLength, _targetDistance, movementSmoothing);
            
            boltMain.startSizeY = nextBoltLength;
            blastMain.startSizeY = nextBoltLength;
        }
    }
}