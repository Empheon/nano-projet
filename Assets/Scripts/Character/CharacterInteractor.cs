using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInteractor : MonoBehaviour
    {
        [SerializeField] private string interactableTag = "Interactable";
        [SerializeField] private string interactingTag = "Interacting";
        [SerializeField] private LayerMask checkLayers;
        [SerializeField] private float checkFrequency = 10;
        [SerializeField] private float checkRadius = 1;

        private readonly Collider2D[] _foundObjects = new Collider2D[20];
        private GameObject _lastClosest;
        private GameObject _closest;
        
        private Gamepad _gamepad;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnSpawn(Gamepad gamepad)
        {
            _gamepad = gamepad;
        }

        private IEnumerator Start()
        {
            for (;;)
            {
                var nbObjectFound = Physics2D.OverlapCircleNonAlloc(_transform.position, checkRadius, _foundObjects, checkLayers);

                _closest = null;
                var distSqrClosest = Mathf.Infinity;

                for (int i = 0; i < nbObjectFound; i++)
                {
                    var foundObject = _foundObjects[i];
                
                    if (!foundObject.CompareTag(interactableTag)) continue;
                
                    var distSqr = (foundObject.transform.position - _transform.position).sqrMagnitude;
                
                    if (distSqr < distSqrClosest)
                    {
                        _closest = foundObject.gameObject;
                        distSqrClosest = distSqr;
                    }
                }

                if (_closest != _lastClosest && _lastClosest!= null)
                {
                    _lastClosest.BroadcastMessage("OnCharacterBlur", gameObject, SendMessageOptions.DontRequireReceiver);
                }

                if (_closest != null)
                {
                    _closest.BroadcastMessage("OnCharacterFocus", gameObject, SendMessageOptions.DontRequireReceiver);
                }
            
                _lastClosest = _closest;
            
                yield return new WaitForSeconds(1 / checkFrequency);
            }
        }

        private void Update()
        {
            if (_gamepad.buttonWest.wasPressedThisFrame)
            {
                if (_closest != null)
                {
                    _closest.BroadcastMessage("OnCharacterInteract", gameObject);
                    
                    // prevent from interacting 2 times with object
                    _closest = null;
                }
                else
                {
                    gameObject.BroadcastMessage("OnNoInteractableFound");
                }
            }

            if (_gamepad.buttonEast.wasPressedThisFrame)
            {
                foreach (var foundObject in _foundObjects)
                {
                    if (foundObject && foundObject.CompareTag(interactingTag))
                    {
                        foundObject.BroadcastMessage("OnStopInteraction");
                    }
                }
            }
        }

#if UNITY_EDITOR
            
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
        
#endif
    }
}