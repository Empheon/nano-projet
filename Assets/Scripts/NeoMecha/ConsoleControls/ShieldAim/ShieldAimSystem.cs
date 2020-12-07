using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animations;
using UnityEngine;

namespace NeoMecha.ConsoleControls.ShieldAim
{
    public class ShieldAimSystem : ConsoleControlSystem
    {
        [SerializeField] private GameObject targetContainer;
        [SerializeField] private ShieldPlacement shieldPlacer;
        [SerializeField] private Shield shield;

        private PositionTarget[] _targets;
        private int _currentTargetIndex = 1;

        private void Start()
        {
            _targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);
            
            shieldPlacer.TurnOff();
        }

        public override bool Activate()
        {
            if (_targets.All(trg => !trg.IsActive)) return false;

            shieldPlacer.TurnOn();
            shieldPlacer.PlaceAt(_targets[_currentTargetIndex].target.position);

            return true;
        }

        public override void Desactivate()
        {
            shieldPlacer.TurnOff();
        }

        public override void Next()
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _targets.Length;
            var target = _targets[_currentTargetIndex];
            
            shieldPlacer.PlaceAt(target.target.position);

            if(!target.IsActive) Next();
        }

        public override void Previous()
        {
            _currentTargetIndex = (_currentTargetIndex + _targets.Length - 1) % _targets.Length;
            var target = _targets[_currentTargetIndex];
            
            shieldPlacer.PlaceAt(target.target.position);
            
            if(!target.IsActive) Previous();
        }

        public override void Validate()
        {
            var target = _targets[_currentTargetIndex];
            
            target.Validate();
            shield.MoveTo(target.target.position);
            
            // so we dont reactivate the system on an already protected target
            Next();
        }
    }
}
