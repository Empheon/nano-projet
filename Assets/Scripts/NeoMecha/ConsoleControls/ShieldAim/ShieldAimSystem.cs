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

        private PositionTarget[] _targets;
        private int _currentTargetIndex;

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
            
            shieldPlacer.PlaceAt(_targets[_currentTargetIndex].target.position);
        }

        public override void Previous()
        {
            _currentTargetIndex = (_currentTargetIndex + _targets.Length - 1) % _targets.Length;
            
            shieldPlacer.PlaceAt(_targets[_currentTargetIndex].target.position);
        }

        public override void Validate()
        {
            _targets[_currentTargetIndex].Validate();
        }
    }
}
