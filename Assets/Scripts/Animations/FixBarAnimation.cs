using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class FixBarAnimation : MonoBehaviour
    {
        [SerializeField]
        private Sprite disabledBar;
        [SerializeField]
        private Sprite redBar;
        [SerializeField]
        private Sprite greenBar;

        private SpriteRenderer m_spriteRenderer;
        private Coroutine m_coroutine;

        private void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnGoRed()
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
            }

            m_spriteRenderer.sprite = redBar;
        }

        public void OnFixComplete()
        {
            m_coroutine = StartCoroutine(Anim());
        }

        private IEnumerator Anim()
        {
            m_spriteRenderer.sprite = greenBar;
            yield return new WaitForSeconds(1);
            m_spriteRenderer.sprite = redBar;
        }
    }
}
