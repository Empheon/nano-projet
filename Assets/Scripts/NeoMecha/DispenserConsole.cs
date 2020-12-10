using Animations;
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
        [SerializeField] private PickableResource resourcePrefab;

        [SerializeField]
        private ConveyerAnimation conveyerAnimation;
        [SerializeField]
        private SupplyCurtainAnimation supplyCurtainAnimation;

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
            var characterResource = character.GetComponent<CharacterResource>();

            // stop player
            characterController.Stop();
            characterController.enabled = false;

            // animate
            characterAnimator.SetTrigger("PushButton");
            PickableResource pickableResource = conveyerAnimation.OnConvey(resourcePrefab.gameObject).GetComponent<PickableResource>();
            yield return new WaitForSeconds(activationDelay);
            
            // player get controls back
            characterController.enabled = true;

            pickableResource.Init();

            // keep track of resource
            m_resources.Add(pickableResource.Resource);
            UpdateCurtainState();
            
            // automatically give resource to character
            characterResource.StoreResource(pickableResource.Resource);

            pickableResource.Resource.OnConsumed += () =>
            {
                m_resources.Remove(pickableResource.Resource);
                UpdateCurtainState();
                Destroy(pickableResource.gameObject);
            };
        }

        public override bool CanInteract(GameObject character)
        {
            return m_resources.Count < maxResourcesNb && _timeSinceLastDispense > dispenseCooldown;
        }

        private void UpdateCurtainState()
        {
            if (m_resources.Count < maxResourcesNb)
            {
                supplyCurtainAnimation.OnOpen();
            } else
            {
                supplyCurtainAnimation.OnClose();
            }
        }
    }
}
