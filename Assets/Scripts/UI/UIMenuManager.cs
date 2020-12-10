using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        private int mechaSceneIndex;
        [SerializeField]
        private int tutoSceneIndex;

        private int nextSceneIndex = -1;

        private void Start()
        {
            HidePanel(gamepadCheckerWrapper, 0);
            HidePanel(settingsWrapper, 0);
        }

        public void OnClickPlay()
        {
            nextSceneIndex = mechaSceneIndex;
            SwitchPanel(titleWrapper, gamepadCheckerWrapper);
        }

        public void OnClickTutorial()
        {
            nextSceneIndex = tutoSceneIndex;
            SwitchPanel(titleWrapper, gamepadCheckerWrapper);
        }

        public void OnClickSettings()
        {
            SwitchPanel(titleWrapper, settingsWrapper);
        }

        public void OnClickBackFrom(GameObject from)
        {
            SwitchPanel(from, titleWrapper);
        }

        public void OnClickQuit()
        {
            Application.Quit();
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

        private void SwitchPanel(GameObject from, GameObject to)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(HidePanel(from));
            seq.Append(ShowPanel(to));
        }

        private Tween HidePanel(GameObject go, float duration = 0.5f)
        {
            return go.transform.DOLocalMoveX(-1920, duration).SetEase(Ease.OutBack).OnComplete(() => go.SetActive(false));
        }

        private Tween ShowPanel(GameObject go, float duration = 0.5f)
        {
            go.SetActive(true);

            MenuAutoSelector menuAutoSelector;
            if (go.TryGetComponent(out menuAutoSelector))
            {
                menuAutoSelector.ObjectToTarget.Select();
            }


            return go.transform.DOLocalMoveX(0, duration).SetEase(Ease.OutBack);
        }
    }
}
