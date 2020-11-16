using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInteractor : MonoBehaviour
    {
        [SerializeField] private string interactableTag = "Interactable";
        
        private Gamepad _gamepad;
        private Transform _transform;
        
        private List<GameObject> _interactableInRange = new List<GameObject>();
        private GameObject _lastClosestInteractable;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnSpawn(Gamepad gamepad)
        {
            _gamepad = gamepad;
        }

        private void Update()
        {
            if (_interactableInRange.Count != 0)
            {
                // update closest interactable in range
                GameObject closest = null;
                
                if(_interactableInRange.Count == 1)
                {
                    closest = _interactableInRange[0];
                }
                else
                {
                    float distSqrClosest = Mathf.Infinity;
            
                    foreach (var interactableGo in _interactableInRange)
                    {
                        float distSqr = (interactableGo.transform.position - _transform.position).sqrMagnitude;
                
                        if (distSqr < distSqrClosest || closest == null)
                        {
                            closest = interactableGo;
                            distSqrClosest = distSqr;
                        }
                    }
                }
                
                if (closest != _lastClosestInteractable)
                {
                    if (_lastClosestInteractable != null)
                    {
                        _lastClosestInteractable.BroadcastMessage("OnCharacterBlur", SendMessageOptions.DontRequireReceiver);
                    }
                    
                    closest.BroadcastMessage("OnCharacterFocus", SendMessageOptions.DontRequireReceiver);
                }

                _lastClosestInteractable = closest;
            }

            if (_gamepad.buttonWest.wasPressedThisFrame && _lastClosestInteractable != null)
            {
                if (_gamepad.buttonWest.wasPressedThisFrame)
                {
                    _lastClosestInteractable.BroadcastMessage("OnCharacterInteractPositive", gameObject);
                } else if (_gamepad.buttonEast.wasPressedThisFrame)
                {
                    _lastClosestInteractable.BroadcastMessage("OnCharacterInteractNegative", gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var obj = other.gameObject;
            if (obj.CompareTag(interactableTag))
            {
                _interactableInRange.Add(obj);
                obj.BroadcastMessage("OnEnterCharacterRange", SendMessageOptions.DontRequireReceiver);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var obj = other.gameObject;
            if (obj.CompareTag(interactableTag))
            {
                _interactableInRange.Remove(obj);
                obj.BroadcastMessage("OnExitCharacterRange", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}