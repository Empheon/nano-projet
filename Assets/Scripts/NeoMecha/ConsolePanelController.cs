using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Character.CharacterController;

namespace NeoMecha
{
    public class ConsolePanelController : MonoBehaviour
    {
        [SerializeField] private string interactingTag = "Interacting";
        [SerializeField] private GameObject uiPanel;
        [SerializeField] [Range(0, 1)] private float getOutValueThreshHold = 0.9f;
        [SerializeField] [Range(0, 1)] private float snapValueThreshHold = 0.5f;
        [SerializeField] private float snapCooldown = 0.2f;
        
        private TargetConsole _console;
        
        private ConsolePanelButton[] _buttons;
        private CharacterController _characterController;
        private Gamepad _gamepad;

        private string _baseTag;
        private int _currentButtonIndex = 0;
        private float _timeSinceLastSnap = 0f;

        private void Awake()
        {
            enabled = false;
            uiPanel.SetActive(false);

            _baseTag = tag;

            _console = GetComponent<TargetConsole>();
            _buttons = GetComponentsInChildren<ConsolePanelButton>(true);
        }

        private void OnCharacterInteract(GameObject character)
        {
            if(!_console.CanDoAction() || !HasAnyButtonActivated()) return;
            
            _characterController = character.GetComponent<CharacterController>();
            _gamepad = _characterController.GetGamepad();
            tag = interactingTag;
            
            EnterPanel();
        }

        private bool HasAnyButtonActivated()
        {
            foreach (var button in _buttons)
            {
                if (button.IsActive) return true;
            }

            return false;
        }

        private void OnStopInteraction()
        {
            ExitPanel();
        }

        private void Update()
        {
            if(!HasAnyButtonActivated() || !_console.CanDoAction()) ExitPanel();
            
            _timeSinceLastSnap += Time.deltaTime;
            
            if(_gamepad == null) return;
            
            var x = _gamepad.leftStick.x.ReadValue();
            
            if (x > getOutValueThreshHold || x < -getOutValueThreshHold)
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

            if (_gamepad.buttonWest.wasPressedThisFrame) Validate();
        }

        private void EnterPanel()
        {
            _characterController.enabled = false;
            
            enabled = true;
            uiPanel.SetActive(true);
            
            var currentButton = _buttons[_currentButtonIndex];
            currentButton.Focus();

            if (!currentButton.IsActive)
            {
                Next();
            }
        }

        private void ExitPanel()
        {
            _characterController.enabled = true;
            _gamepad = null;
            tag = _baseTag;

            uiPanel.SetActive(false);
            enabled = false;
            
            var currentButton = _buttons[_currentButtonIndex];
            currentButton.Blur();
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
            
            if(!nextButton.IsActive) Next();
            else _timeSinceLastSnap = 0;
        }

        private void Previous()
        {
            var currentButton = _buttons[_currentButtonIndex];
            _currentButtonIndex = (_currentButtonIndex + _buttons.Length - 1) % _buttons.Length;
            var prevButton = _buttons[_currentButtonIndex];
            
            currentButton.Blur();
            prevButton.Focus();
            
            if(!prevButton.IsActive) Previous();
            else _timeSinceLastSnap = 0;
        }
    }
}