using System.Linq;
using Animations;
using UnityEngine;

namespace NeoMecha.ConsoleControls.JammerAim
{
    public class JammerAimSystem : ConsoleControlSystem
    {
        [SerializeField] private Jammer jammer;
        [SerializeField] private GameObject targetContainer;

        private PositionTarget[] _targets;
        private int _currentTargetIndex;

        private void Start()
        {
            _targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);
            jammer.TurnOff();
        }

        public override bool Activate()
        {
            if (_targets.All(trg => !trg.IsActive)) return false;
            
            jammer.TurnOn();
            jammer.AimAt(_targets[_currentTargetIndex].target.position);
            
            return true;
        }

        public override void Desactivate()
        {
            jammer.TurnOff();
        }

        public override void Next()
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _targets.Length;
            
            jammer.AimAt(_targets[_currentTargetIndex].target.position);
        }

        public override void Previous()
        {
            _currentTargetIndex = (_currentTargetIndex + _targets.Length - 1) % _targets.Length;

            jammer.AimAt(_targets[_currentTargetIndex].target.position);
        }

        public override void Validate()
        {
            _targets[_currentTargetIndex].Validate();
            jammer.Blast();
        }
    }
}