﻿using UnityEngine;
using DG.Tweening;
using Character;

namespace NeoMecha
{
    public class ConsoleTinter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        private Color m_defaultColor;

        private bool m_isFocused;
        private bool m_isLight;

        private CharacterResource m_currentCharacterResource;
        private Console m_linkedConsole;

        protected virtual void Start()
        {
            m_defaultColor = m_spriteRenderer.color;
            m_linkedConsole = GetComponent<Console>();
        }

        private void OnCharacterBlur(GameObject character)
        {
            m_currentCharacterResource = character.GetComponent<CharacterResource>();
            m_isFocused = false;
        }

        private void OnCharacterFocus(GameObject character)
        {
            m_currentCharacterResource = character.GetComponent<CharacterResource>();
            m_isFocused = true;
        }

        protected virtual void Update()
        {
            if (m_currentCharacterResource == null)
            {
                return;
            }

            if (!m_isLight && m_isFocused && m_linkedConsole.CanInteract(m_currentCharacterResource))
            {
                m_isLight = true;
                m_spriteRenderer.DOColor(m_defaultColor * 1.5f, 0.2f);
            } else if (m_isLight && (!m_isFocused || !m_linkedConsole.CanInteract(m_currentCharacterResource)))
            {
                m_isLight = false;
                m_spriteRenderer.DOColor(m_defaultColor, 0.2f);
            }
        }
    }
}