using UnityEngine;

namespace Resources
{
    public struct Resource
    {
        public ResourceTypes Type;
        public GameObject GO;

        public Resource(ResourceTypes type, GameObject go)
        {
            Type = type;
            GO = go;
        }
    }
    
    public enum ResourceTypes
    {
        None,
        Energy,
        Ammunition
    }
}