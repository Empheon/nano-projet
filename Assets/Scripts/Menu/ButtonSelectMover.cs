using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Menu
{
    public class ButtonSelectMover : MonoBehaviour
    {
        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        public void OnSelect()
        {
            m_animator.SetBool("Selected", true);
        }

        public void OnDeselect()
        {
            m_animator.SetBool("Selected", false);
        }
    }
}
