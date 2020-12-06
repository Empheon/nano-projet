using System;
using System.Collections;
using UnityEngine;

namespace Animations
{
    public class Jammer : MonoBehaviour
    {
        [SerializeField] private Transform jammerHandle;
        [SerializeField] private Transform jammerOrigin;
        
        [SerializeField] private ParticleSystem teslaCoil;
        [SerializeField] private ParticleSystem electricBolt;
        [SerializeField] private ParticleSystem electricBoltBlast;
        [SerializeField] private ParticleSystem impactBlast;
        
        [SerializeField] private Vector3 offset;
        [SerializeField] [Range(0f, 1f)] private float movementSmoothing;
        
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private float _targetDistance;
        private Vector3 _boldScale;
        
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
            _targetPosition = point;

            var direction = jammerOrigin.InverseTransformPoint(point);
            
            _targetRotation = Quaternion.LookRotation(direction);
            _targetDistance = direction.magnitude;
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
            _boldScale = electricBolt.transform.lossyScale;
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