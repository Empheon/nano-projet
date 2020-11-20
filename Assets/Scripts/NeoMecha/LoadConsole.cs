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
    public class LoadConsole : Console
    {
        [SerializeField]
        private ResourceTypes resourceType;
        [HideInInspector]
        public bool IsLoaded;

        public void OnCharacterInteract(GameObject character)
        {
            if (IsLoaded) return;
            
            CharacterResource characterResource = character.GetComponent<CharacterResource>();

            if (characterResource.HasResource(resourceType))
            {
                characterResource.ConsumeResource(transform.position);
                IsLoaded = true;
            }
        }

        protected override bool CanInteract(CharacterResource characterResource)
        {
            return !IsLoaded && characterResource.HasResource(resourceType);
        }
    }
}
