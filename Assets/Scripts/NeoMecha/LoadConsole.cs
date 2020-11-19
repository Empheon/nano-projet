using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Character;
using Resources;

namespace NeoMecha
{
    public class LoadConsole : MonoBehaviour
    {
        [SerializeField]
        private ResourceTypes resourceType;
        [HideInInspector]
        public bool IsLoaded;

        public void OnCharacterInteract(GameObject character)
        {
            CharacterResource characterResource = character.GetComponent<CharacterResource>();

            if (characterResource.HasResource(resourceType))
            {
                characterResource.ConsumeResource(transform.position);
                IsLoaded = true;
            }
        }
    }
}
