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
using TMPro;
using UnityEngine.Events;

namespace UI
{
    public class IntroManager : MonoBehaviour
    {
        [SerializeField] private List<SplashScreenItem> splashScreenItems;

        [SerializeField] private GameObject menuCanvas;
        [SerializeField]
        private GameObject EventSystem;

        private const float DEFAULT_FADE_DURATION = 0.8f;
        private Coroutine m_coroutine;

        private static bool m_wasPlayed = false;

        public UnityEvent OnStartIntro;
        public UnityEvent OnFinishIntro;

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
                splashScreenItem.Wrapper.SetActive(true);
            }

            HideAllScreens(0);
        }

        private void Start()
        {
            m_coroutine = StartCoroutine(RunIntro());
        }

        private IEnumerator RunIntro()
        {

            for (int i = 0; i < splashScreenItems.Count; i++)
            {
                HideAllScreens();
                yield return new WaitForSeconds(DEFAULT_FADE_DURATION);

                var item = splashScreenItems[i];
                ShowScreen(item);

                if (i == 0)
                {
                    // Wait before everything loads (especially the sound)
                    yield return new WaitForSeconds(0.3f);
                    OnStartIntro.Invoke();
                }

                item.Wrapper.transform.DOScale(item.ToScale, item.Duration).From(item.FromScale).SetEase(Ease.Linear);


                yield return new WaitForSeconds(item.Duration - DEFAULT_FADE_DURATION);
            }

            HideAllScreens();
            yield return new WaitForSeconds(DEFAULT_FADE_DURATION);
            FinishIntro();
        }

        private void HideAllScreens(float duration = DEFAULT_FADE_DURATION)
        {
            foreach (var splashScreenItem in splashScreenItems)
            {
                foreach (Image img in splashScreenItem.Wrapper.GetComponentsInChildren<Image>())
                {
                    img.DOFade(0, duration).SetEase(Ease.Linear);
                }
                foreach (TextMeshProUGUI text in splashScreenItem.Wrapper.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.DOFade(0, duration).SetEase(Ease.Linear);
                }
            }
        }

        private void ShowScreen(SplashScreenItem item, float duration = DEFAULT_FADE_DURATION)
        {
            foreach (Image img in item.Wrapper.GetComponentsInChildren<Image>())
            {
                img.DOFade(1, duration).SetEase(Ease.Linear);
            }
            foreach (TextMeshProUGUI text in item.Wrapper.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.DOFade(1, duration).SetEase(Ease.Linear);
            }
        }

        private void Update()
        {
            if (Gamepad.all.Any((g) => g.buttonWest.wasPressedThisFrame)
                || InputSystem.devices.Any((g) => {
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
                foreach (Image img in splashScreenItem.Wrapper.GetComponentsInChildren<Image>())
                {
                    img.DOKill();
                }
                foreach (TextMeshProUGUI text in splashScreenItem.Wrapper.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.DOKill();
                }
            }

            OnFinishIntro.Invoke();

            m_wasPlayed = true;
            menuCanvas.SetActive(true);
            EventSystem.SetActive(true);
            Destroy(gameObject);
        }

    }

    [Serializable]
    public class SplashScreenItem
    {
        public GameObject Wrapper;
        public float Duration;
        public float FromScale = 1;
        public float ToScale = 1;
    }
}
