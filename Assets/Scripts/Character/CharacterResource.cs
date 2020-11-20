using UnityEngine;
using Resources;
using System.Collections;
using DG.Tweening;

namespace Character
{
    public class CharacterResource : MonoBehaviour
    {
        [SerializeField] private Vector2 storedResourcePosOffset = Vector2.zero;
        [SerializeField] private float followSmoothing = 0.2f;
        
        private Resource _storedResource;

        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (_storedResource != null)
            {
                var nextPos = Vector2.Lerp(
                    _storedResource.GO.transform.position, 
                    _transform.TransformPoint(storedResourcePosOffset), 
                    followSmoothing);
                
                _storedResource.GO.transform.position = nextPos;
            }
        }

        public void StoreResource(Resource resource)
        {
            _storedResource?.GO.BroadcastMessage("OnStopInteraction");
            _storedResource = resource;
        }

        public bool HasResource(ResourceTypes ofType)
        {
            if (_storedResource == null) return false;
            return _storedResource.Type == ofType;
        }

        public Resource ConsumeResource(Vector3 moveToPosition)
        {
            _storedResource.GO.transform.DOScale(0,0.4f);
            _storedResource.GO.transform.DOMove(moveToPosition, 0.4f);
            
            StartCoroutine(ConsumeResourceDelayed(_storedResource));
            
            return _storedResource;
        }

        private IEnumerator ConsumeResourceDelayed(Resource resource)
        {
            yield return new WaitForEndOfFrame();
            _storedResource = null;
            yield return new WaitForSeconds(1);
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