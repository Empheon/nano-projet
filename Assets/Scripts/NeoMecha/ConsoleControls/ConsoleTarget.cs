using UnityEngine;
using UnityEngine.Events;

namespace NeoMecha.ConsoleControls
{
    public abstract class ConsoleTarget : MonoBehaviour
    {
        public UnityEvent OnValidate;

        public abstract void Activate();
        public abstract void Desactivate();
        public abstract void Validate();
    }
}