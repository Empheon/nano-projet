using System;
using System.Linq;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class GamepadCheckManager : MonoBehaviour
    {
        //[SerializeField] private int sceneIndex = -1;
        
        private GamepadCheck[] _checkers;
        //private int _nextSceneIndex;

        public UnityEvent LoadNextScene;
        
        private void OnEnable()
        {
            _checkers = GetComponentsInChildren<GamepadCheck>();
        }

        private bool EveryOneIsReady()
        {
            return _checkers.All(checker => checker.IsReady);
        }

        private void Update()
        {
            if (EveryOneIsReady())
            {
                PlayerManager.Instance.Locked = true;
                LoadNextScene.Invoke();
                //SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}