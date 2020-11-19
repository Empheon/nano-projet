using System;
using UnityEngine;

namespace Resources
{
    public class Resource
    {
        public ResourceTypes Type;
        public GameObject GO;

        public Action OnConsumed;

        public Resource(ResourceTypes type, GameObject go)
        {
            Type = type;
            GO = go;
        }

        public void Consume()
        {
            OnConsumed?.Invoke();
        }
    }
    
    public enum ResourceTypes
    {
        None,
        Energy,
        Ammunition
    }
}