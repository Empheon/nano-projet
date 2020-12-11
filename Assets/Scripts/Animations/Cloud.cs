using System;
using UnityEngine;

namespace Animations
{
    public class Cloud : MonoBehaviour
    {
        public float speed = 0f;

        private void Update()
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
    }
}