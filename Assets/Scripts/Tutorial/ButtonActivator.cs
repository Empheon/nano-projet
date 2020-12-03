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
        private GameObject m_button;

        public void Init(GameObject button)
        {
            m_button = button;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_button != null && !m_button.activeSelf)
            {
                m_button.SetActive(true);
                m_button.transform.DOScale(1f, 0.5f).From(0).SetEase(Ease.InOutQuad);
                //TODO anim
            }
        }
    }
}
