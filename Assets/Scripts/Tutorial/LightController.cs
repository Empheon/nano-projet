using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

namespace Tutorial
{
    public class LightController : MonoBehaviour
    {
        private const float DEFAULT_DURATION = 0.5f;

        private Light2D m_light;
        [HideInInspector]
        public Color Color;

        private Coroutine m_coroutine;
        private bool isOn;

        private void Awake()
        {
            m_light = GetComponent<Light2D>();
        }

        public void SwitchOn(float duration = DEFAULT_DURATION)
        {
            isOn = true;
            DOTween.To(() => m_light.color, (x) => m_light.color = x, Color, duration);
        }

        public void SwitchOff(float duration = DEFAULT_DURATION)
        {
            isOn = false;
            DOTween.To(() => m_light.color, (x) => m_light.color = x, Color.black, duration);
        }

        public void SwitchOnClean(float duration = DEFAULT_DURATION)
        {
            DOTween.To(() => m_light.color, (x) => m_light.color = x, Color, duration);
        }

        public void SwitchOffClean(float duration = DEFAULT_DURATION)
        {
            DOTween.To(() => m_light.color, (x) => m_light.color = x, Color.black, duration);
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
            while(true)
            {
                SwitchOn();
                yield return new WaitForSeconds(DEFAULT_DURATION);
                SwitchOff();
                yield return new WaitForSeconds(DEFAULT_DURATION);
            }
        }
    }
}
