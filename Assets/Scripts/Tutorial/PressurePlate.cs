using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Tutorial
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField]
        private Transform button;

        private bool m_isEnabled;
        private bool m_isPressed;

        public UnityEvent OnButtonPressed;

        private void Start()
        {
            transform.localScale = Vector3.zero;
        }

        public void Show()
        {
            m_isEnabled = true;
            transform.DOScale(1, 0.5f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!m_isEnabled || m_isPressed) return;
            m_isPressed = true;

            button.DOLocalMoveY(-0.08f, 0.2f);
            OnButtonPressed.Invoke();
        }
    }
}
