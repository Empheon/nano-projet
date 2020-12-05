using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Global
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        
        [SerializeField] private float gamepadFetchFrequency = 5;

        public ReadOnlyArray<Player> Players { get; private set; }

        private List<GamepadAdapter> _gamepads;
        private List<IGameController> _keyboards;
        private IGameController[] _controllers;

        private bool _locked;
        private Coroutine _fetchControllersRoutine;
        public bool Locked
        {
            get => _locked;
            set
            {
                if (value) StopCoroutine(_fetchControllersRoutine);
                else _fetchControllersRoutine = StartCoroutine(FetchControllers());

                _locked = value;
            }
        }

        public Player[] GetAvailablePlayers()
        {
            return Players.Where(player => player.GameController.IsConnected()).ToArray();
        }

        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("Player manager already exist, destructing current");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;

            // get keyboards controls
            IGameController arrows = new ArrowsKeyboardAdapter();
            IGameController zqsd = new ZQSDKeyboardAdapter();
            _keyboards = new List<IGameController>{ arrows, zqsd };
            
            // setup gamepads
            _gamepads = new List<GamepadAdapter>();
            
            // get gamepads every X seconds
            _fetchControllersRoutine = StartCoroutine(FetchControllers());
        }

        private void Update()
        {
            foreach (var controller in _controllers)
            {
                controller.UpdateState();
            }
        }

        private IEnumerator FetchControllers()
        {
            for (;;)
            {
                // add gamepad that do not exist
                foreach (var gamepad in Gamepad.all)
                {
                    if (!_gamepads.Exists(adapter => adapter.Gamepad == gamepad))
                    {
                        _gamepads.Add(new GamepadAdapter(gamepad));
                    }
                }
                
                // remove disconnected gamepads
                foreach (var adapter in _gamepads)
                {
                    // if the adapter gamepad does not exist anymore
                    if (!Gamepad.all.ToList().Exists(gpd => gpd == adapter.Gamepad))
                    {
                        _gamepads.Remove(adapter);
                    }
                }

                _controllers = _gamepads.Union(_keyboards).ToArray();
                
                // build players
                Players = _controllers
                    .Where(controller => controller.IsConnected())
                    .Select((controller, index) => new Player {
                        GameController = controller,
                        Team = index % 2 == 0 ? Team.Left : Team.Right,
                    })
                    .ToArray();
                
                yield return new WaitForSeconds(1 / gamepadFetchFrequency);
            }
        }

        public struct Player
        {
            public IGameController GameController;
            public Team Team;
        }

        public enum Team
        {
            Left,
            Right,
        }
    }
}