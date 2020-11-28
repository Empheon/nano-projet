using UnityEngine;

namespace NeoMecha.ConsoleControls
{
    [RequireComponent(typeof(ConsoleControls))]
    public abstract class ConsoleControlSystem : MonoBehaviour
    {
        public abstract bool Activate();
        public abstract void Desactivate();
        public abstract void Next();
        public abstract void Previous();
        public abstract void Validate();
    }
}