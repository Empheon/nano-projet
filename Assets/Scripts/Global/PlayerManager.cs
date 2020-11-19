using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Global
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        
        [SerializeField] private float gamepadFetchFrequency = 5;
        
        public ReadOnlyArray<Player> Players { get; private set; } = new ReadOnlyArray<Player>();

        private bool _locked = false;
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

        private Coroutine _fetchControllersRoutine;
        
        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("Player manager already exist, destructing current");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            _fetchControllersRoutine = StartCoroutine(FetchControllers());
        }

        private IEnumerator FetchControllers()
        {
            for (;;)
            {
                Players = Gamepad.all.Select((gamePad, index) => new Player {
                    gamepad = gamePad,
                    team = index % 2 == 0 ? Team.Left : Team.Right,
                }).ToArray();
            
                yield return new WaitForSeconds(1 / gamepadFetchFrequency);
            }
        }

        public struct Player
        {
            public Gamepad gamepad;
            public Team team;
        }

        public enum Team
        {
            Left,
            Right,
        }
    }
}