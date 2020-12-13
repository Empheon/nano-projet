using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Global.Loading;
using System.Collections;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject titleWrapper;
        [SerializeField]
        private GameObject gamepadCheckerWrapper;
        [SerializeField]
        private GameObject settingsWrapper;
        [SerializeField]
        private GameObject creditsWrapper;

        [SerializeField]
        private GameObject logo;

        [SerializeField]
        private int mechaSceneIndex;
        [SerializeField]
        private int tutoSceneIndex;

        private int nextSceneIndex = -1;

        private void Start()
        {
            HidePanel(gamepadCheckerWrapper, 0);
            HidePanel(settingsWrapper, 0);
            HidePanel(creditsWrapper, 0);
        }

        public void OnClickPlay()
        {
            nextSceneIndex = mechaSceneIndex;
            SwitchPanel(titleWrapper, gamepadCheckerWrapper);
            HidePanel(logo, 0.5f, -1);
        }

        public void OnClickTutorial()
        {
            nextSceneIndex = tutoSceneIndex;
            SwitchPanel(titleWrapper, gamepadCheckerWrapper);
            HidePanel(logo, 0.5f, -1);
        }

        public void OnClickSettings()
        {
            SwitchPanel(titleWrapper, settingsWrapper);
        }

        public void OnClickCredits()
        {
            SwitchPanel(titleWrapper, creditsWrapper);
            HidePanel(logo, 0.5f, -1);
        }

        public void OnClickBackFrom(GameObject from)
        {
            SwitchPanel(from, titleWrapper);
            StartCoroutine(DelayedShowLogo(1));
        }

        private IEnumerator DelayedShowLogo(float delay)
        {
            yield return new WaitForSeconds(delay);
            ShowPanel(logo, 0.7f, Ease.OutBack);
        }

        public void OnClickQuit()
        {
            Application.Quit();
        }

        public void LoadNextScene()
        {
            LoadingScreen.Instance.LoadScene(nextSceneIndex);
        }

        private void SwitchPanel(GameObject from, GameObject to)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(HidePanel(from));
            seq.Append(ShowPanel(to));
        }

        private Tween HidePanel(GameObject go, float duration = 0.5f, int sens = 1)
        {
            return go.transform.DOLocalMoveX(1920 * sens, duration).SetEase(Ease.OutQuad).OnComplete(() => go.SetActive(false));
        }

        private Tween ShowPanel(GameObject go, float duration = 0.5f, Ease easing = Ease.OutQuad)
        {
            go.SetActive(true);

            MenuAutoSelector menuAutoSelector;
            if (go.TryGetComponent(out menuAutoSelector))
            {
                menuAutoSelector.ObjectToTarget.Select();
            }


            return go.transform.DOLocalMoveX(0, duration).SetEase(easing);
        }
    }
}
