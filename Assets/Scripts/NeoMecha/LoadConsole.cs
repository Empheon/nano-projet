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
        [SerializeField] private ResourceTypes resourceType;
        [SerializeField] private Transform loadTo;
        
        [HideInInspector] public bool IsLoaded;

        public void OnCharacterInteract(GameObject character)
        {
            if (IsLoaded) return;
            
            CharacterResource characterResource = character.GetComponent<CharacterResource>();

            if (characterResource.HasResource(resourceType))
            {
                characterResource.ConsumeResource(loadTo.position);
                IsLoaded = true;
            }
        }

        public override bool CanInteract(GameObject character)
        {
            var characterResource = character.GetComponent<CharacterResource>();
            return !IsLoaded && characterResource.HasResource(resourceType);
        }
    }
}
