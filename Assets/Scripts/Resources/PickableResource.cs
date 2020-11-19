using System;
using UnityEngine;
using Character;

namespace Resources
{
    public class PickableResource : MonoBehaviour
    {
        [SerializeField] private string interactingTag = "Interacting";
        [SerializeField] private ResourceTypes type = ResourceTypes.None;

        private string _baseTag;
        private CharacterResource _characterResource;

        private Rigidbody2D _rb;

        public Resource ResourceObject;

        public void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
            _baseTag = tag;
            ResourceObject = new Resource(type, gameObject);
        }

        private void OnCharacterInteract(GameObject character)
        {
            _characterResource = character.GetComponent<CharacterResource>();

            _rb.simulated = false;
            tag = interactingTag;

            _characterResource.StoreResource(ResourceObject);
        }

        private void OnStopInteraction()
        {
            _rb.simulated = true;
            tag = _baseTag;
            _characterResource.LetResourceDown();
        }
        
    }
}