using System;
using System.Collections.Generic;
using System.Linq;
using Animations;
using UnityEngine;

namespace NeoMecha.ConsoleControls.LaserAim
{
    public class LaserAimSystem : ConsoleControlSystem
    {
        [SerializeField] private Laser laser;
        [SerializeField] private GameObject targetContainer;

        private PositionTarget[] _targets;
        private int _currentTargetIndex;

        private void Start()
        {
            _targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);
            laser.TurnOff();
        }

        public override bool Activate()
        {
            if (_targets.All(trg => !trg.IsActive)) return false;
            
            laser.TurnOn();
            laser.AimAt(_targets[_currentTargetIndex].target.position);
            
            return true;
        }

        public override void Desactivate()
        {
            laser.TurnOff();
        }

        public override void Next()
        {
            _currentTargetIndex = Mathf.Min(_currentTargetIndex + 1, _targets.Length - 1);
            
            laser.AimAt(_targets[_currentTargetIndex].target.position);
        }

        public override void Previous()
        {
            _currentTargetIndex = Mathf.Max(_currentTargetIndex - 1, 0);

            laser.AimAt(_targets[_currentTargetIndex].target.position);
        }

        public override void Validate()
        {
            _targets[_currentTargetIndex].Validate();
            laser.Shoot();
        }
    }
}