using UnityEngine;
using Resources;
using System.Collections;

namespace Character
{
    public class CharacterResource : MonoBehaviour
    {
        [SerializeField] private Vector2 storedResourcePosOffset = Vector2.zero;
        [SerializeField] private float followSmoothing = 0.2f;
        
        private Resource _storedResource = null;

        delegate Vector2 PositionGetter();
        private PositionGetter _getTargetObjectPos = () => Vector2.zero;
        
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (_storedResource != null)
            {
                var objTransform = _storedResource.GO.transform;
                objTransform.position = Vector2.Lerp(objTransform.position, _getTargetObjectPos(), followSmoothing);
            }
        }

        public void StoreResource(Resource resource)
        {
            _storedResource?.GO.BroadcastMessage("OnStopInteraction");

            _getTargetObjectPos = () => _transform.TransformPoint(storedResourcePosOffset);
            _storedResource = resource;
        }

        public bool HasResource(ResourceTypes ofType)
        {
            if (_storedResource == null) return false;
            return _storedResource.Type == ofType;
        }

        public Resource ConsumeResource(Vector3 moveToPosition)
        {
            _getTargetObjectPos = () => moveToPosition;
            StartCoroutine(ConsumeResourceDelayed(_storedResource));
            return _storedResource;
        }

        private IEnumerator ConsumeResourceDelayed(Resource resource)
        {
            yield return new WaitForSeconds(1);
            _storedResource = null;
            resource.Consume();
        }

        public void LetResourceDown()
        {
            _storedResource = null;
        }

        private void OnNoInteractableFound()
        {
            _storedResource?.GO.BroadcastMessage("OnStopInteraction");
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.TransformPoint(storedResourcePosOffset), 0.2f);
        }
        
#endif
    }
}