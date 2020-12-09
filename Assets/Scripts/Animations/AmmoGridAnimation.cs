using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class AmmoGridAnimation : MonoBehaviour
    {
        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            OnUnload();
        }

        public void OnLoad()
        {
            m_animator.SetBool("IsOpened", false);
        }

        public void OnUnload()
        {
            m_animator.SetBool("IsOpened", true);
        }
    }
}
