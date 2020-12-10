using System;
using System.Linq;
using UnityEngine;

namespace NeoMecha.ConsoleControls.ButtonControl
{
    public class ButtonControlAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        
        private ButtonTarget[] _buttons;
        private int m_previousIndex;

        private void Start()
        {
            _buttons = panel.GetComponentsInChildren<ButtonTarget>(true);
            panel.SetActive(false);
        }

        public bool Activate(int index)
        {
            panel.SetActive(true);
            
            var currentButton = _buttons[index];
            currentButton.Focus();
            
            return true;
        }

        public void Desactivate(int index)
        {
            var currentButton = _buttons[index];
            currentButton.Blur();
            panel.SetActive(false);
        }

        public void Focus(int index)
        {
            var currentButton = _buttons[m_previousIndex];
            var nextButton = _buttons[index];
            
            currentButton.Blur();
            nextButton.Focus();

            m_previousIndex = index;
        }
    }
}