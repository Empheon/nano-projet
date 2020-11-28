using UnityEngine;

namespace NeoMecha.ConsoleControls
{
    public class LaserTarget : ConsoleTarget
    {
        [SerializeField] private Transform target;
        public bool IsActive { get; private set; }

        public override void Activate()
        {
            IsActive = true;
        }

        public override void Desactivate()
        {
            IsActive = false;
        }

        public override void Validate()
        {
            OnValidate.Invoke();
        }
    }
}