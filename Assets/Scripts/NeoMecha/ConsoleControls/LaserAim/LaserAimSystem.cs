using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeoMecha.ConsoleControls.LaserAim
{
    public class LaserAimController : ConsoleControlSystem
    {
        private LaserTarget[] _targets;
        private int _currentButtonIndex;

        private void Start()
        {
            _targets = GetComponentsInChildren<LaserTarget>(true);
        }

        public override bool Activate()
        {
            return true;
        }

        public override void Desactivate()
        {
            
        }

        public override void Next()
        {
            
        }

        public override void Previous()
        {
            
        }

        public override void Validate()
        {
            
        }
    }
}