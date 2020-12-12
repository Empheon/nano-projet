using System;
using System.Collections;
using System.Collections.Generic;
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
                _eventSent = true;
                StartCoroutine(WaitAndLoadNextScene());
            }
        }

        private IEnumerator WaitAndLoadNextScene()
        {
            yield return new WaitForSeconds(0.5f);
            LoadNextScene.Invoke();
        }
    }
}