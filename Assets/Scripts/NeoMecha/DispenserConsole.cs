using Character;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace NeoMecha
{
    public class DispenserConsole : Console
    {
        [SerializeField] private int maxResourcesNb;
        [SerializeField] private float activationDelay;
        [SerializeField] private Vector2 velocity;
        [SerializeField] private PickableResource resourcePrefab;

        private List<Resource> m_resources;

        protected void Start()
        {
            m_resources = new List<Resource>();
        }

        public void OnCharacterInteract(GameObject character)
        {
            if (m_resources.Count < maxResourcesNb)
            {
                StartCoroutine(DoInteraction(character));
            }
        }

        private IEnumerator DoInteraction(GameObject character)
        {
            var characterAnimator = character.GetComponentInChildren<Animator>();
            var characterController = character.GetComponent<CharacterController>();

            characterController.enabled = false;
            characterAnimator.SetTrigger("PushButton");
            
            yield return new WaitForSeconds(activationDelay);
            
            characterController.enabled = true;
            
            // create resource
            PickableResource pickableResource = Instantiate(resourcePrefab, transform.position, Quaternion.identity);
            pickableResource.Init();
            
            var resourceRb = pickableResource.GetComponent<Rigidbody2D>();
            resourceRb.AddForce(velocity, ForceMode2D.Impulse);
            
            // keep track of resource
            pickableResource.ResourceObject.OnConsumed += () => ConsumeResource(pickableResource);
            m_resources.Add(pickableResource.ResourceObject);
        }


        private void ConsumeResource(PickableResource pickableResource)
        {
            m_resources.Remove(pickableResource.ResourceObject);
            Destroy(pickableResource.gameObject);
        }

        public override bool CanInteract(CharacterResource characterResource)
        {
            return m_resources.Count < maxResourcesNb;
        }
    }
}
