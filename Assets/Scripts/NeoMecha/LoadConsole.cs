using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Character;
using Resources;
using UnityEngine.Events;

namespace NeoMecha
{
    public class LoadConsole : Console
    {
        [SerializeField] private ResourceTypes resourceType;
        public bool IsLoaded { get; private set; }

        public UnityEvent OnLoad;
        public UnityEvent OnUnload;

        public void OnCharacterInteract(GameObject character)
        {
            if (IsLoaded) return;
            
            CharacterResource characterResource = character.GetComponent<CharacterResource>();

            if (characterResource.HasResource(resourceType))
            {
                var resource = characterResource.GetResource();
                characterResource.LetResourceDown();
                resource.Consume();

                switch (resourceType)
                {
                    case ResourceTypes.Ammunition:
                        AkSoundEngine.PostEvent("Load_ENE_In_DEF_Room", gameObject);
                        break;
                    case ResourceTypes.Energy: 
                        AkSoundEngine.PostEvent("Load_MUN_In_ATK_Room", gameObject);
                        break;
                }

                IsLoaded = true;
                OnLoad.Invoke();
            }
        }

        public void UnLoad()
        {
            IsLoaded = false;
            OnUnload.Invoke();
        }

        public override bool CanInteract(GameObject character)
        {
            var characterResource = character.GetComponent<CharacterResource>();
            return !IsLoaded && characterResource.HasResource(resourceType);
        }
    }
}
