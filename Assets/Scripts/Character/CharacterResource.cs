using UnityEngine;
using Resources;

namespace Character
{
    public class CharacterResource : MonoBehaviour
    {
        [SerializeField] private Vector2 storedResourcePosOffset = Vector2.zero;

        private Resource _storedResource = new Resource(ResourceTypes.None, null);
        
        public void StoreResource(Resource resource) {}
        public void HasResource(ResourceTypes ofType) {}
        public Resource ConsumeResource(Vector3 moveToPosition) { return _storedResource; }
        
#if UNITY_EDITOR
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.TransformPoint(storedResourcePosOffset), 0.2f);
        }
        
#endif
    }
}