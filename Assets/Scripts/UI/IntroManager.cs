using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class IntroManager : MonoBehaviour
    {
        [SerializeField] private List<SplashScreenItem> splashScreenItems;

        [SerializeField] private GameObject menuCanvas;
        [SerializeField]
        private GameObject EventSystem;

        private readonly float m_checkFrequency = 0.01f;
        private Coroutine m_coroutine;

        private static bool m_wasPlayed = false;

        private void Awake()
        {
            if (m_wasPlayed)
            {
                FinishIntro();
                return;
            }
            EventSystem.SetActive(false);
            menuCanvas.SetActive(false);

            foreach (var splashScreenItem in splashScreenItems)
            {
                splashScreenItem.Wrapper.SetActive(false);
            }
        }

        private void Start()
        {
            m_coroutine = StartCoroutine(RunIntro());
        }

        private IEnumerator RunIntro()
        {

            for (int i = 0; i < splashScreenItems.Count; i++)
            {
                foreach (var splashScreenItem in splashScreenItems)
                {
                    splashScreenItem.Wrapper.SetActive(false);
                }
                var item = splashScreenItems[i];
                item.Wrapper.SetActive(true);

                if (i == 0)
                {
                    // Wait before everything loads (especially the sound)
                    yield return new WaitForSeconds(0.5f);
                }

                item.Wrapper.transform.DOScale(1.1f, item.Duration).From(1).SetEase(Ease.Linear);

                yield return new WaitForSeconds(item.Duration);
            }
            FinishIntro();
        }

        private void Update()
        {
            if (Gamepad.all.Any((g) => g.buttonWest.wasPressedThisFrame)
                || InputSystem.devices.Any((g) =>
                {
                    if (g is Keyboard keyboard)
                    {
                        return keyboard.spaceKey.wasPressedThisFrame;
                    }

                    return false;
                }))
            {
                StopCoroutine(m_coroutine);
                FinishIntro();
            }
        }

        private void FinishIntro()
        {
            foreach (var splashScreenItem in splashScreenItems)
            {
                splashScreenItem.Wrapper.transform.DOKill();
            }

            m_wasPlayed = true;
            menuCanvas.SetActive(true);
            EventSystem.SetActive(true);
            Destroy(gameObject);
        }

    }

    [Serializable]
    public struct SplashScreenItem
    {
        public GameObject Wrapper;
        public float Duration;
    }
}
