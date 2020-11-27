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

        private bool m_isPressed;

        public UnityEvent OnButtonPressed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_isPressed) return;
            m_isPressed = true;

            button.DOLocalMoveY(-0.08f, 0.2f);
            OnButtonPressed.Invoke();
        }
    }
}
