using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Character.CharacterController;

namespace NeoMecha
{
    public class ConsolePanelController : MonoBehaviour
    {
        [SerializeField] private GameObject uiPanel;
        [SerializeField] [Range(0, 1)] private float getOutValueThreshHold = 0.9f;
        [SerializeField] [Range(0, 1)] private float snapValueThreshHold = 0.5f;
        [SerializeField] private float snapCooldown = 0.2f;
        
        private ConsolePanelButton[] _buttons;
        private CharacterController _characterController;
        private Gamepad _gamepad;

        private int _currentButtonIndex = 0;
        private float _timeSinceLastSnap = 0f;

        private void Awake()
        {
            enabled = false;
            uiPanel.SetActive(false);

            _buttons = GetComponentsInChildren<ConsolePanelButton>(true);
        }

        private void OnCharacterInteract(GameObject character)
        {
            _characterController = character.GetComponent<CharacterController>();
            _gamepad = _characterController.GetGamepad();
            
            EnterPanel();
        }

        private void Update()
        {
            _timeSinceLastSnap += Time.deltaTime;
            
            if(_gamepad == null) return;
            
            var x = _gamepad.leftStick.x.ReadValue();
            
            if (_gamepad.buttonEast.wasPressedThisFrame || x > getOutValueThreshHold || x < -getOutValueThreshHold)
            {
                ExitPanel();
                return;
            }

            if (_timeSinceLastSnap > snapCooldown)
            {
                var y = _gamepad.leftStick.y.ReadValue();

                if (y > snapValueThreshHold) Previous();
                else if (y < -snapValueThreshHold) Next();
            }

            if (_gamepad.buttonSouth.wasPressedThisFrame) Validate();
        }

        private void EnterPanel()
        {
            _characterController.enabled = false;
            
            enabled = true;
            uiPanel.SetActive(true);
            
            var currentButton = _buttons[_currentButtonIndex];
            currentButton.Focus();
        }

        private void ExitPanel()
        {
            _characterController.enabled = true;
            _gamepad = null;
            
            uiPanel.SetActive(false);
            enabled = false;
        }

        private void Validate()
        {
            var currentButton = _buttons[_currentButtonIndex];
            currentButton.OnValidate.AddListener(ExitPanel);
            currentButton.Validate();
        }

        private void Next()
        {
            var currentButton = _buttons[_currentButtonIndex];
            _currentButtonIndex = (_currentButtonIndex + 1) % _buttons.Length;
            var nextButton = _buttons[_currentButtonIndex];
            
            currentButton.Blur();
            nextButton.Focus();
            
            if(!nextButton.enabled) Next();

            _timeSinceLastSnap = 0;
        }

        private void Previous()
        {
            var currentButton = _buttons[_currentButtonIndex];
            _currentButtonIndex = (_currentButtonIndex + _buttons.Length - 1) % _buttons.Length;
            var prevButton = _buttons[_currentButtonIndex];
            
            currentButton.Blur();
            prevButton.Focus();
            
            if(!prevButton.enabled) Previous();
            
            _timeSinceLastSnap = 0;
        }
    }
}