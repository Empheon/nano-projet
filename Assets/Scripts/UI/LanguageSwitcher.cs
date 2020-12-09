using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using YorfLib;
using Global;

namespace UI
{
    public class LanguageSwitcher : Selectable
    {
        [SerializeField]
        private List<string> languageKeys;
        [SerializeField]
        private TranslateTMP languageText;

        private int m_keyIndex;

        protected override void Start()
        {
            base.Start();
            m_keyIndex = languageKeys.IndexOf(Settings.Instance.LanguageKey);
        }

        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
            if (eventData.moveDir == MoveDirection.Left)
            {
                m_keyIndex = mod((m_keyIndex - 1), languageKeys.Count);
            } else if (eventData.moveDir == MoveDirection.Right)
            {
                m_keyIndex = mod((m_keyIndex + 1), languageKeys.Count);
            }

            Settings.Instance.SetLanguage(languageKeys[m_keyIndex]);
        }

        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
