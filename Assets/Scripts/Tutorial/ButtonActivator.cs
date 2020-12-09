using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Tutorial
{
    public class ButtonActivator : MonoBehaviour
    {
        private TutorialStepChecker m_button;

        public void Init(TutorialStepChecker button)
        {
            m_button = button;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_button != null && !m_button.IsEnabled)
            {
                m_button.Enable();
                m_button.transform.DOScale(1f, 0.5f).From(0).SetEase(Ease.InOutQuad);
                //TODO anim
            }
        }
    }
}
