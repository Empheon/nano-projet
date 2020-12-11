using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animations
{
    public class CloudGenerator : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private Vector2 spawnFrom;
        [SerializeField] private float spawnFrequency = 0.8f;
        [SerializeField] private float randomSpawnFactor = 0.5f;
        
        [Header("Clouds")]
        [SerializeField] private List<Cloud> clouds;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        
        private IEnumerator Start()
        {
            for (;;)
            {
                // spawn the cloud
                var spawnPos = new Vector2(
                    Random.Range(transform.position.x - spawnFrom.x, transform.position.x + spawnFrom.x),
                    Random.Range(transform.position.y - spawnFrom.y, transform.position.y + spawnFrom.y)
                );

                var cloudPrefab = clouds[Random.Range(0, clouds.Count)];
            
                Cloud cloud = Instantiate(cloudPrefab, spawnPos, Quaternion.identity);
            
                // set cloud speed
                cloud.speed = Random.Range(minSpeed, maxSpeed);
            
                // wait for next cloud spawn (with a bit of randomness)
                yield return new WaitForSeconds(1 / spawnFrequency + Random.Range(-randomSpawnFactor, randomSpawnFactor));
            }
        }


#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, spawnFrom);
        }

#endif
    }
}