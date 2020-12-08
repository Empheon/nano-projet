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
                    animator.SetBool("HoldAmmo", true);
                    animator.SetBool("HoldEnergy", false);
                    break;
                case ResourceTypes.Energy:
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
            _storedResource.GameObject.transform.position = _transform.position;
            _storedResource.GameObject.SetActive(true);
            _storedResource = null;
            
            animator.SetBool("HoldAmmo", false);
            animator.SetBool("HoldEnergy", false);
        }
    }
}