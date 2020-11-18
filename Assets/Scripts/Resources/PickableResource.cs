using System;
using UnityEngine;
using Character;

namespace Resources
{
    public class PickableResource : MonoBehaviour
    {
        [SerializeField] private string duringInteractionTag = "Untagged";
        [SerializeField] private ResourceTypes type = ResourceTypes.None;

        private string _baseTag;
        
        private Rigidbody2D _rb;

        [HideInInspector]
        public Resource ResourceObject;

        public void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
            _baseTag = gameObject.tag;

            ResourceObject = new Resource(type, gameObject);
        }

        private void OnCharacterInteract(GameObject character)
        {
            var characterResource = character.GetComponent<CharacterResource>();

            _rb.simulated = false;
            gameObject.tag = duringInteractionTag;

            characterResource.StoreResource(ResourceObject);
        }

        private void OnLetResourceDown(Vector2 baseVelocity)
        {
            _rb.simulated = true;
            _rb.velocity = baseVelocity;
            gameObject.tag = _baseTag;
        }
        
    }
}