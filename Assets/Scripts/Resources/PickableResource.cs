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

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _baseTag = gameObject.tag;
        }

        private void OnCharacterInteract(GameObject character)
        {
            var resource = new Resource(type, gameObject);
            var characterResource = character.GetComponent<CharacterResource>();

            _rb.simulated = false;
            gameObject.tag = duringInteractionTag;

            characterResource.StoreResource(resource);
        }

        private void OnLetResourceDown(Vector2 baseVelocity)
        {
            _rb.simulated = true;
            _rb.velocity = baseVelocity;
            gameObject.tag = _baseTag;
        }
        
    }
}