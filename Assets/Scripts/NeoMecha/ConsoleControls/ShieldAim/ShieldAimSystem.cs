using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NeoMecha.ConsoleControls.ShieldAim
{
    public class ShieldAimSystem : ConsoleControlSystem
    {
        [SerializeField] private GameObject targetContainer;

        private PositionTarget[] m_targets;

        private void Start()
        {
            m_targets = targetContainer.GetComponentsInChildren<PositionTarget>(true);

        }

        public override bool Activate()
        {
            if (m_targets.All(trg => !trg.IsActive)) return false;


            return true;
        }

        public override void Desactivate()
        {
            throw new NotImplementedException();
        }

        public override void Next()
        {
            throw new NotImplementedException();
        }

        public override void Previous()
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
