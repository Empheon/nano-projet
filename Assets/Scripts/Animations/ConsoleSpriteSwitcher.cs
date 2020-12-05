using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Animations
{
    public class ConsoleSpriteSwitcher : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteHolder;

        [SerializeField]
        private Sprite activeConsole;
        [SerializeField]
        private Sprite disabledConsole;
        [SerializeField]
        private Sprite brokenConsole;

        private Coroutine m_coroutine;

        public void OnFix()
        {
            spriteHolder.sprite = disabledConsole;
        }

        public void OnBreak()
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
                m_coroutine = null;
            }

            spriteHolder.sprite = brokenConsole;
        }

        public void OnActivate(float duration)
        {
            m_coroutine = StartCoroutine(ActivateAnim(duration));
        }

        private IEnumerator ActivateAnim(float duration)
        {
            spriteHolder.sprite = activeConsole;

            yield return new WaitForSeconds(duration);

            spriteHolder.sprite = disabledConsole;
        }
    }
}
