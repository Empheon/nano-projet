﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animations;
using NeoMecha.ConsoleControls.ButtonControl;
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
        
        [SerializeField] private ButtonControlAnimation buttonControlAnimation;

        private void Start()
        {
            _targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);
            
            shieldPlacer.TurnOff();
        }

        public override bool Activate()
        {
            if (_targets.All(trg => !trg.IsActive)) return false;

            // go back and re-check if room is targetable
            _currentTargetIndex--;
            Next();
            
            shieldPlacer.TurnOn();
            shieldPlacer.PlaceAt(_targets[_currentTargetIndex].target.position);

            buttonControlAnimation.Activate(_currentTargetIndex);
            buttonControlAnimation.Focus(_currentTargetIndex);

            return true;
        }

        public override void Desactivate()
        {
            shieldPlacer.TurnOff();

            buttonControlAnimation.Desactivate(_currentTargetIndex);
        }

        public override void Next()
        {
            _currentTargetIndex = Mathf.Min(_currentTargetIndex + 1, _targets.Length - 1);
            var target = _targets[_currentTargetIndex];
            
            shieldPlacer.PlaceAt(target.target.position);

            buttonControlAnimation.Focus(_currentTargetIndex);

            if (!target.IsActive)
            {
                if(_currentTargetIndex == _targets.Length - 1) Previous();
                else Next();
            }
        }

        public override void Previous()
        {
            _currentTargetIndex = Mathf.Max(_currentTargetIndex - 1, 0);
            var target = _targets[_currentTargetIndex];
            
            shieldPlacer.PlaceAt(target.target.position);

            buttonControlAnimation.Focus(_currentTargetIndex);

            if (!target.IsActive)
            {
                if(_currentTargetIndex == 0) Next();
                else Previous();
            }
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
