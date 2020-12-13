using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Global.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        public static LoadingScreen Instance;

        public UnityEvent OnStartLoading;

        private Animator _animator;
        private int _targetSceneIndex = -1;

        private bool m_isLoading;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Loading Screen already exist, destructing current");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;

            _animator = GetComponent<Animator>();
        }

        public void LoadScene(int sceneIndex)
        {
            if (m_isLoading) return;
            m_isLoading = true;

            _targetSceneIndex = sceneIndex;
            _animator.SetBool("Loading", true);
            AkSoundEngine.PostEvent("LoadingScreen_Door_Close", gameObject);

            OnStartLoading.Invoke();
        }

        public void OnLoadingAnimationFinished()
        {
            var op = SceneManager.LoadSceneAsync(_targetSceneIndex);
            op.completed += operation =>
            {
                AkSoundEngine.StopAll();
                _animator.SetBool("Loading", false);
                AkSoundEngine.PostEvent("LoadingScreen_Door_Open", gameObject);
                _targetSceneIndex = -1;
                m_isLoading = false;
            };
        }

    }
}