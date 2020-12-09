using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class SupplyCurtainAnimation : MonoBehaviour
    {
        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            OnOpen();
        }

        public void OnOpen()
        {
            m_animator.SetBool("IsOpen", true);
        }

        public void OnClose()
        {
            m_animator.SetBool("IsOpen", false);
        }
    }
}
