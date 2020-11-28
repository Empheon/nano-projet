using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NeoMecha.ConsoleControls.LaserAim
{
    public class LaserAimSystem : ConsoleControlSystem
    {
        [SerializeField] private GameObject targetContainer;
        [SerializeField] private GameObject laserParticles;
        [SerializeField] private Transform laser;
        [SerializeField] private Vector3 laserRotationOffset;
        
        
        private PositionTarget[] _targets;
        private int _currentButtonIndex;

        private void Start()
        {
            _targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);
            laserParticles.SetActive(false);
        }

        public override bool Activate()
        {
            if (_targets.All(trg => !trg.IsActive)) return false;
            
            laserParticles.SetActive(true);
            return true;
        }

        public override void Desactivate()
        {
            laserParticles.SetActive(false);
        }

        public override void Next()
        {
            _currentButtonIndex = (_currentButtonIndex + 1) % _targets.Length;
            var target = _targets[_currentButtonIndex];
            
            laser.LookAt(target.target);
            laser.Rotate(laserRotationOffset);
        }

        public override void Previous()
        {
            _currentButtonIndex = (_currentButtonIndex + _targets.Length - 1) % _targets.Length;
            var target = _targets[_currentButtonIndex];

            laser.LookAt(target.target);
            laser.Rotate(laserRotationOffset);
        }

        public override void Validate()
        {
            var target = _targets[_currentButtonIndex];
            target.Validate();
        }
    }
}