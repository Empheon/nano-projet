using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Animations
{
    public class ConveyerAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject supplyPrefab;

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private float offset;
        [SerializeField]
        private int supplyNumber;

        [SerializeField]
        private float duration;

        private Queue<GameObject> m_supplyObjects;
        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_supplyObjects = new Queue<GameObject>(supplyNumber);

            for (int i = supplyNumber - 1; i >= 0; i--)
            {
                Vector3 newPosition = startPosition.position;
                newPosition.x += i * offset;

                m_supplyObjects.Enqueue(Instantiate(supplyPrefab, newPosition, Quaternion.identity));
            }
        }

        public GameObject OnConvey(GameObject prefabToInstantiate)
        {
            Vector3 newPosition = startPosition.position;
            newPosition.x += (supplyNumber - 1) * offset;
            GameObject go = Instantiate(prefabToInstantiate, newPosition, Quaternion.identity);
            StartCoroutine(Convey(go));

            return go;
        }

        private IEnumerator Convey(GameObject resourceObject)
        {
            m_animator.SetBool("IsConveying", true);

            var lastGo = m_supplyObjects.Dequeue();
            lastGo.SetActive(false);

            foreach (var go in m_supplyObjects)
            {
                go.transform.DOMoveX(go.transform.position.x + offset, duration);
            }
            resourceObject.transform.DOMoveX(resourceObject.transform.position.x + offset * 1.5f, duration);

            yield return new WaitForSeconds(duration);

            
            lastGo.transform.position = startPosition.position;
            lastGo.SetActive(true);
            m_supplyObjects.Enqueue(lastGo);

            m_animator.SetBool("IsConveying", false);
        }

    }
}
