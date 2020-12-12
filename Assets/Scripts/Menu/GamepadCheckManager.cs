using System;
using System.Linq;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class GamepadCheckManager : MonoBehaviour
    {
        private GamepadCheck[] _checkers;
        private bool _eventSent;

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
            if (EveryOneIsReady() && !_eventSent)
            {
                PlayerManager.Instance.Locked = true;
                LoadNextScene.Invoke();
                _eventSent = true;
            }
        }
    }
}