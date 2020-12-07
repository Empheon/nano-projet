using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Animations
{
    public class LightController : MonoBehaviour
    {
        [SerializeField, ColorUsage(true, true)]
        protected Color red;
        [SerializeField, ColorUsage(true, true)]
        protected Color green;
        [SerializeField, ColorUsage(true, true)]
        protected Color yellow;

        [SerializeField, ColorUsage(true, true)]
        protected Color off;

        [SerializeField]
        protected Light2D m_light;
        protected SpriteRenderer m_spriteRenderer;

        protected Color m_currentColor;

        private const float DEFAULT_DURATION = 0.5f;

        private Coroutine m_coroutine;
        private bool isOn;

        private void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            InitialState();
        }

        protected virtual void InitialState()
        {
            SwitchOff(0);
        }

        public void ChangeColor(LightColor lightColor)
        {
            switch (lightColor)
            {
                case LightColor.RED:
                    m_currentColor = red;
                    break;
                case LightColor.GREEN:
                    m_currentColor = green;
                    break;
                case LightColor.YELLOW:
                    m_currentColor = yellow;
                    break;
            }
        }

        public void SwitchOn(float duration = DEFAULT_DURATION)
        {
            isOn = true;
            SwitchOnClean(duration);
        }

        public void SwitchOff(float duration = DEFAULT_DURATION)
        {
            isOn = false;
            SwitchOffClean(duration);
        }

        public void SwitchOnClean(float duration = DEFAULT_DURATION)
        {
            DOTween.To(() => m_light.color, (x) => m_light.color = x, m_currentColor, duration);
            DOTween.To(() => m_spriteRenderer.material.color, (x) => m_spriteRenderer.material.color = x, m_currentColor, duration);
        }

        public void SwitchOffClean(float duration = DEFAULT_DURATION)
        {
            DOTween.To(() => m_light.color, (x) => m_light.color = x, Color.black, duration);
            DOTween.To(() => m_spriteRenderer.material.color, (x) => m_spriteRenderer.material.color = x, off, duration);
        }

        public void StartBlink()
        {
            m_coroutine = StartCoroutine(Blink());
        }

        public void StopBlink()
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
                if (isOn)
                {
                    SwitchOnClean();
                } else
                {
                    SwitchOffClean();
                }
            }
        }

        private IEnumerator Blink()
        {
            while (true)
            {
                SwitchOnClean();
                yield return new WaitForSeconds(DEFAULT_DURATION);
                SwitchOffClean();
                yield return new WaitForSeconds(DEFAULT_DURATION);
            }
        }
    }

    [Serializable]
    public enum LightColor
    {
        RED, GREEN, YELLOW
    }
}
