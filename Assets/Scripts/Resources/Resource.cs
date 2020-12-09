using System;
using UnityEngine;

namespace Resources
{
    public class Resource
    {
        public readonly ResourceTypes Type;
        public readonly GameObject GameObject;

        public Action OnConsumed;

        public Resource(ResourceTypes type, GameObject gameObject)
        {
            Type = type;
            GameObject = gameObject;
        }

        public void Consume()
        {
            OnConsumed?.Invoke();
        }
    }
    
    public enum ResourceTypes
    {
        Energy,
        Ammunition
    }
}