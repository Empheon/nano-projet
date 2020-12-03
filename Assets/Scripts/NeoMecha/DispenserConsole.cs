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
        [SerializeField] private float dispenseCooldown;
        [SerializeField] private Transform dispenseFrom;
        [SerializeField] private Vector2 velocity;
        [SerializeField] private Transform snapPosition;
        [SerializeField] private PickableResource resourcePrefab;

        private List<Resource> m_resources;
        private float _timeSinceLastDispense;

        protected void Start()
        {
            m_resources = new List<Resource>();
            _timeSinceLastDispense = dispenseCooldown;
        }

        private void Update()
        {
            _timeSinceLastDispense += Time.deltaTime;
        }

        public void OnCharacterInteract(GameObject character)
        {
            if (CanInteract(character))
            {
                _timeSinceLastDispense = 0f;
                StartCoroutine(DoInteraction(character));
            }
        }

        private IEnumerator DoInteraction(GameObject character)
        {
            var characterAnimator = character.GetComponentInChildren<Animator>();
            var characterController = character.GetComponent<CharacterController>();

            // stop player
            characterController.Stop();
            characterController.enabled = false;

            // snap player to right place
            character.transform.position = snapPosition.position;
            
            // animate
            characterAnimator.SetTrigger("PushButton");
            yield return new WaitForSeconds(activationDelay);
            
            // player get controls back
            characterController.enabled = true;
            
            // create resource
            PickableResource pickableResource = Instantiate(resourcePrefab, dispenseFrom.position, Quaternion.identity);
            pickableResource.Init();
            
            var resourceRb = pickableResource.GetComponent<Rigidbody2D>();
            resourceRb.AddForce(velocity, ForceMode2D.Impulse);
            
            // keep track of resource
            m_resources.Add(pickableResource.ResourceObject);
            
            pickableResource.ResourceObject.OnConsumed += () =>
            {
                m_resources.Remove(pickableResource.ResourceObject);
                Destroy(pickableResource.gameObject);
            };
        }

        public override bool CanInteract(GameObject character)
        {
            return m_resources.Count < maxResourcesNb && _timeSinceLastDispense > dispenseCooldown;
        }
    }
}
