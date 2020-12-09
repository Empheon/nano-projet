using System;
using UnityEngine;
using Resources;
using System.Collections;
using DG.Tweening;
using Inputs;

namespace Character
{
    public class CharacterResource : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private Resource _storedResource;
        private Transform _transform;
        private IGameController _gc;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        private void OnSpawn(IGameController gamepad)
        {
            _gc = gamepad;
        }

        private void OnNoInteractableFound()
        {
            if(_storedResource != null) LetResourceDown();
        }

        private void Update()
        {
            if (_storedResource != null && _gc.CancelThisFrame())
            {
                LetResourceDown();
            }
        }

        public void StoreResource(Resource resource)
        {
            if(_storedResource != null) LetResourceDown();
            
            _storedResource = resource;
            _storedResource.GameObject.SetActive(false);
            
            switch (_storedResource.Type)
            {
                case ResourceTypes.Ammunition:
                    AkSoundEngine.PostEvent("Take_MUN_In_Munition_Room", gameObject);
                    animator.SetBool("HoldAmmo", true);
                    animator.SetBool("HoldEnergy", false);
                    break;
                case ResourceTypes.Energy:
                    AkSoundEngine.PostEvent("Take_ENE_In_Energy_Room", gameObject);
                    animator.SetBool("HoldAmmo", false);
                    animator.SetBool("HoldEnergy", true);
                    break;
            }
        }

        public bool HasResource(ResourceTypes ofType)
        {
            if (_storedResource == null) return false;
            return _storedResource.Type == ofType;
        }

        public Resource GetResource()
        {
            return _storedResource;
        }

        public void LetResourceDown()
        {
            if (_storedResource.GameObject != null)
            {
                _storedResource.GameObject.transform.position = _transform.position;
                _storedResource.GameObject.SetActive(true);
            }
            _storedResource = null;
            
            animator.SetBool("HoldAmmo", false);
            animator.SetBool("HoldEnergy", false);
        }
    }
}