using UnityEngine;
using Character;

namespace Resources
{
    public class PickableResource : MonoBehaviour
    {

        [SerializeField] private ResourceTypes type;
        
        private void OnCharacterInteract(GameObject character)
        {
            var resource = new Resource(type, gameObject);
            var characterResource = character.GetComponent<CharacterResource>();
            
            characterResource.StoreResource(resource);
        }
        
    }
}