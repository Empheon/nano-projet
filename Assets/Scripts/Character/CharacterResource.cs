using UnityEngine;
using Resources;

namespace Character
{
    public class CharacterResource : MonoBehaviour
    {
        [SerializeField] private Vector2 storedResourcePosOffset = Vector2.zero;
        [SerializeField] private float followSmoothing = 0.2f;
        
        private Resource _storedResource = new Resource(ResourceTypes.None, null);

        delegate Vector2 PositionGetter();
        private PositionGetter _getTargetObjectPos = () => Vector2.zero;
        
        private Transform _transform;
        private Rigidbody2D _rb;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_storedResource.GO != null)
            {
                var objTransform = _storedResource.GO.transform;
                objTransform.position = Vector2.Lerp(objTransform.position, _getTargetObjectPos(), followSmoothing);
            }
        }

        public void StoreResource(Resource resource)
        {
            if (_storedResource.GO != null)
            {
                _storedResource.GO.BroadcastMessage("OnLetResourceDown", _rb.velocity);
            }
            
            _getTargetObjectPos = () => _transform.TransformPoint(storedResourcePosOffset);
            _storedResource = resource;
        }

        public bool HasResource(ResourceTypes ofType)
        {
            return _storedResource.Type == ofType;
        }

        public Resource ConsumeResource(Vector3 moveToPosition)
        {
            _getTargetObjectPos = () => moveToPosition;
            return _storedResource;
        }

        private void OnNoInteractableFound()
        {
            if (_storedResource.GO != null)
            {
                _storedResource.GO.BroadcastMessage("OnLetResourceDown", _rb.velocity);
                _storedResource = new Resource(ResourceTypes.None, null);
            }
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