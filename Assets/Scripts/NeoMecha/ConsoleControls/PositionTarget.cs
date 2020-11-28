using UnityEngine;

namespace NeoMecha.ConsoleControls
{
    public class PositionTarget : ConsoleTarget
    {
        public Transform target;

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