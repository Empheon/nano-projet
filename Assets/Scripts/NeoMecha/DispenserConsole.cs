using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NeoMecha
{
    public class DispenserConsole : MonoBehaviour
    {
        [SerializeField]
        private int maxResourcesNb;

        [SerializeField]
        private Vector2 velocity;

        [SerializeField]
        private PickableResource resourcePrefab;

        private List<Resource> m_resources;

        public void OnCharacterInteract(GameObject character)
        {
            if (m_resources.Count >= maxResourcesNb)
            {
                return;
            }

            PickableResource pickableResource = Instantiate(resourcePrefab, transform.position, Quaternion.identity);
            pickableResource.Init();
            pickableResource.GetComponent<Rigidbody>().AddForce(velocity * transform.forward, ForceMode.Impulse);

            pickableResource.ResourceObject.OnConsumed += () => m_resources.Remove(pickableResource.ResourceObject);
            m_resources.Add(pickableResource.ResourceObject);
        }
    }
}
