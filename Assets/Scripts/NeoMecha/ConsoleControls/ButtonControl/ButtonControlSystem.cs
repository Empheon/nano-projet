using System;
using System.Linq;
using UnityEngine;

namespace NeoMecha.ConsoleControls.ButtonControl
{
    public class ButtonControlSystem : ConsoleControlSystem
    {
        [SerializeField] private GameObject panel;
        
        private int _currentButtonIndex;
        private ButtonTarget[] _buttons;

        private void Start()
        {
            _buttons = GetComponentsInChildren<ButtonTarget>(true);
            panel.SetActive(false);
        }

        public override bool Activate()
        {
            if (_buttons.All(btn => !btn.IsActive)) return false;
            
            panel.SetActive(true);
            return true;
        }

        public override void Desactivate()
        {
            panel.SetActive(false);
        }

        public override void Next()
        {
            var currentButton = _buttons[_currentButtonIndex];
            _currentButtonIndex = (_currentButtonIndex + 1) % _buttons.Length;
            var nextButton = _buttons[_currentButtonIndex];
            
            currentButton.Blur();
            nextButton.Focus();
            
            if(!nextButton.IsActive) Next();
        }

        public override void Previous()
        {
            var currentButton = _buttons[_currentButtonIndex];
            _currentButtonIndex = (_currentButtonIndex + _buttons.Length - 1) % _buttons.Length;
            var prevButton = _buttons[_currentButtonIndex];
            
            currentButton.Blur();
            prevButton.Focus();
            
            if(!prevButton.IsActive) Previous();
        }

        public override void Validate()
        {
            _buttons[_currentButtonIndex].Validate();
        }
    }
}