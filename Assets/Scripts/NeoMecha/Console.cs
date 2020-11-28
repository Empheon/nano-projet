using UnityEngine;
using Character;

namespace NeoMecha
{
    public abstract class Console : MonoBehaviour
    {
        public abstract bool CanInteract(CharacterResource characterResource);
    }
}
