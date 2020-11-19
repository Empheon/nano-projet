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
        
        public Rigidbody2D Rb { get; private set; }

        [HideInInspector]
        public Resource ResourceObject;

        public void Init()
        {
            Rb = GetComponent<Rigidbody2D>();
            _baseTag = gameObject.tag;
            ResourceObject = new Resource(type, gameObject);
        }

        private void OnCharacterInteract(GameObject character)
        {
            var characterResource = character.GetComponent<CharacterResource>();

            Rb.simulated = false;
            gameObject.tag = duringInteractionTag;

            characterResource.StoreResource(ResourceObject);
        }

        private void OnLetResourceDown(Vector2 baseVelocity)
        {
            Rb.simulated = true;
            Rb.velocity = baseVelocity;
            gameObject.tag = _baseTag;
        }
        
    }
}