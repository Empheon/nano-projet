using System;
using UnityEngine;
using Character;

namespace Resources
{
    public class PickableResource : MonoBehaviour
    {
        public ResourceTypes type = ResourceTypes.Ammunition;
        
        public Resource Resource;

        public void Init()
        {
            Resource = new Resource(type, gameObject);
        }

        private void OnCharacterInteract(GameObject character)
        {
            character
                .GetComponent<CharacterResource>()
                .StoreResource(Resource);
        }
        
    }
}