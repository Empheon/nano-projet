using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * this scripts redirects the played scene to the
 * preload scene and then back to the wanted scene
 *
 * All objects in preload scene are global managers
 * that need to startup before game starts.
 *
 * build index 0 must always be the preload scene.
 */
namespace Global
{
    public class PreloadManager : MonoBehaviour
    {
        [SerializeField] private GameObject globalGO;

        private void Awake()
        {
            DontDestroyOnLoad(globalGO);
        }

#if UNITY_EDITOR // if we are building, we dont need this system to run

        private static bool _gameStartedOnPreload = true;
        private static int _goBackToScene = 0;

        private void Start()
        {
            if (_gameStartedOnPreload)
            {
                SceneManager.LoadScene(1);
                return;
            }

            SceneManager.LoadScene(_goBackToScene);
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitLoadingScene()
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex != 0)
            {
                _gameStartedOnPreload = false;
                _goBackToScene = sceneIndex;
                SceneManager.LoadScene(0);
            }
        }

#else
    private void Start()
    {
        SceneManager.LoadScene(1);
    }

#endif
    }
}