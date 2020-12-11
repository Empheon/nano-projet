using System.Linq;
using Animations;
using NeoMecha.ConsoleControls.ButtonControl;
using UnityEngine;

namespace NeoMecha.ConsoleControls.JammerAim
{
    public class JammerAimSystem : ConsoleControlSystem
    {
        [SerializeField] private Jammer jammer;
        [SerializeField] private GameObject targetContainer;

        private PositionTarget[] _targets;
        private int _currentTargetIndex;

        [SerializeField] private ButtonControlAnimation buttonControlAnimation;

        private void Start()
        {
            _targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);
            jammer.TurnOff();
        }

        public override bool Activate()
        {
            if (_targets.All(trg => !trg.IsActive)) return false;
            
            // go back and re-check if room is targetable
            _currentTargetIndex--;
            Next();
            
            jammer.TurnOn();
            jammer.AimAt(_targets[_currentTargetIndex].target.position);

            buttonControlAnimation.Activate(_currentTargetIndex);
            buttonControlAnimation.Focus(_currentTargetIndex);

            return true;
        }

        public override void Desactivate()
        {
            jammer.TurnOff();

            buttonControlAnimation.Desactivate(_currentTargetIndex);
        }

        public override void Next()
        {
            _currentTargetIndex = Mathf.Min(_currentTargetIndex + 1, _targets.Length - 1);
            var target = _targets[_currentTargetIndex];
            
            jammer.AimAt(target.target.position);

            buttonControlAnimation.Focus(_currentTargetIndex);

            if (!target.IsActive)
            {
                // if at the end go back and loop (works because we only have 3 targets)
                if (_currentTargetIndex == _targets.Length - 1)
                {
                    _currentTargetIndex = -1;
                }
                    
                Next();
            }
        }

        public override void Previous()
        {
            _currentTargetIndex = Mathf.Max(_currentTargetIndex - 1, 0);
            var target = _targets[_currentTargetIndex];

            jammer.AimAt(target.target.position);

            buttonControlAnimation.Focus(_currentTargetIndex);
            
            if (!target.IsActive)
            {
                // if at the end go back and loop (works because we only have 3 targets)
                if (_currentTargetIndex == 0)
                {
                    _currentTargetIndex = _targets.Length;
                }
                    
                Previous();
            }
        }

        public override void Validate()
        {
            _targets[_currentTargetIndex].Validate();
            jammer.Blast();
        }
    }
}