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
            
            jammer.AimAt(_targets[_currentTargetIndex].target.position);

            buttonControlAnimation.Focus(_currentTargetIndex);
        }

        public override void Previous()
        {
            _currentTargetIndex = Mathf.Max(_currentTargetIndex - 1, 0);

            jammer.AimAt(_targets[_currentTargetIndex].target.position);

            buttonControlAnimation.Focus(_currentTargetIndex);
        }

        public override void Validate()
        {
            _targets[_currentTargetIndex].Validate();
            jammer.Blast();
        }
    }
}