using System;
using System.Collections;
using DG.Tweening;
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
        
        [SerializeField] [Range(0f, 1f)] private float movementSmoothing;
        
        private Vector3 _targetPosition;
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
            jammerHandle.DOLookAt(point, 0.5f);
            _targetDistance = Vector3.Distance(jammerHandle.position, point) / electricBolt.transform.lossyScale.x;
            _targetPosition = point;
        }

        public void Blast()
        {
            electricBoltBlast.Play();

            impactBlast.transform.position = _targetPosition;
            impactBlast.Play();
        }
        
        private void Awake()
        {
            _targetDistance = electricBolt.main.startSizeX.constant;
        }

        private void Update()
        {
            // lerp bolt lenght
            var boltMain = electricBolt.main;
            var blastMain = electricBoltBlast.main;
            var boltLength = boltMain.startSizeY.constant;
            var nextBoltLength = Mathf.Lerp(boltLength, _targetDistance, movementSmoothing);
            
            boltMain.startSizeY = nextBoltLength;
            blastMain.startSizeY = nextBoltLength;
        }
    }
}