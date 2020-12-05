using System;
using UnityEngine;

namespace Animations
{
    public class ShieldPlacement : MonoBehaviour
    {
        [SerializeField] private Transform placeholderObject;
        [SerializeField] private Transform verticalAnchor;
        [SerializeField] [Range(0, 1)] private float movementSpeed;

        private Vector3 _targetPos;
        
        public void TurnOn()
        {
            placeholderObject.gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            placeholderObject.gameObject.SetActive(false);
        }

        public void PlaceAt(Vector2 position)
        {
            _targetPos = new Vector3(verticalAnchor.position.x, position.y);
        }

        private void Update()
        {
            placeholderObject.position = Vector2.Lerp(placeholderObject.position, _targetPos, movementSpeed);
        }
    }
}