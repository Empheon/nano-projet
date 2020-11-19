using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace NeoMecha
{
    public abstract class Console : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        private Color m_defaultColor;

        private bool m_isFocused;
        private bool m_isLight;

        protected virtual void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_defaultColor = m_spriteRenderer.color;
        }

        private void OnCharacterBlur()
        {
            m_isFocused = false;
        }

        private void OnCharacterFocus()
        {
            m_isFocused = true;
        }

        protected abstract bool CanInteract();

        protected virtual void Update()
        {
            if (!m_isLight && m_isFocused && CanInteract())
            {
                m_isLight = true;
                m_spriteRenderer.DOColor(m_defaultColor * 1.3f, 0.2f);
            } else if (m_isLight && (!m_isFocused || !CanInteract()))
            {
                m_isLight = false;
                m_spriteRenderer.DOColor(m_defaultColor, 0.2f);
            }
        }
    }
}
