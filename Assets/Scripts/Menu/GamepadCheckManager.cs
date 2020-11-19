using System;
using System.Linq;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class GamepadCheckManager : MonoBehaviour
    {
        [SerializeField] private int sceneIndex = -1;
        
        private GamepadCheck[] _checkers;
        private int _nextSceneIndex;
        
        private void Start()
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
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}