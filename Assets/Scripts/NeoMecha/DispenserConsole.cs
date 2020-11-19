using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NeoMecha
{
    public class DispenserConsole : Console
    {
        [SerializeField]
        private int maxResourcesNb;

        [SerializeField]
        private Vector2 velocity;

        [SerializeField]
        private PickableResource resourcePrefab;

        private List<Resource> m_resources;

        protected override void Start()
        {
            base.Start();
            m_resources = new List<Resource>();
        }

        public void OnCharacterInteract(GameObject character)
        {
            if (m_resources.Count >= maxResourcesNb)
            {
                return;
            }

            PickableResource pickableResource = Instantiate(resourcePrefab, transform.position, Quaternion.identity);
            pickableResource.Init();
            
            var resourceRb = pickableResource.GetComponent<Rigidbody2D>();
            resourceRb.AddForce(velocity, ForceMode2D.Impulse);

            pickableResource.ResourceObject.OnConsumed += () => ConsumeResource(pickableResource);
            m_resources.Add(pickableResource.ResourceObject);
        }


        private void ConsumeResource(PickableResource pickableResource)
        {
            m_resources.Remove(pickableResource.ResourceObject);
            Destroy(pickableResource.gameObject);
        }

        protected override bool CanInteract()
        {
            return m_resources.Count < maxResourcesNb;
        }
    }
}
