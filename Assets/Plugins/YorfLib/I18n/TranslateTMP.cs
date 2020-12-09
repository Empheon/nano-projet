using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System.Text;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace YorfLib
{
	/// <summary>
	/// Translates Text components
	/// </summary>
	[ExecuteInEditMode]
	public class TranslateTMP : MonoBehaviour
	{
		private TMP_Text textComponent;

		[SerializeField, TextArea]
		private string m_text;
		public string Text { get { return m_text; } set { m_text = value; Refresh(); } }

		private readonly static Regex s_regex = new Regex("\\<i18n=\"(?<key>\\w+)\"\\>");

		private readonly static float m_refreshTime = 0.05f;
		private float m_refreshCounter;

		private void OnEnable()
		{
			textComponent = GetComponent<TMP_Text>();
			Refresh();
		}

		public void Refresh(bool refreshTMP = false)
		{
			if (textComponent == null)
			{
				return;
			}

			if (m_text != null && m_text.Length > 0)
			{
				textComponent.text = s_regex.Replace(m_text, (m) => { return I18n.GetString(m.Groups[1].Captures[0].Value); });
			}
			else
			{
				textComponent.text = "";
			}

			if (refreshTMP)
			{
				textComponent.ForceMeshUpdate();
			}
		}

        private void Update()
        {
			// Could be replaced by events for efficiency
			if (m_refreshCounter > m_refreshTime)
			{
				Refresh();
				m_refreshCounter = 0;
			} else
            {
				m_refreshCounter += Time.deltaTime;
            }
        }

        public static string GetI18nKey(string key)
		{
			StringBuilder builder = new StringBuilder("<i18n=\"\">".Length + key.Length);
			builder.Append("<i18n=\"");
			builder.Append(key);
			builder.Append("\">");

			return builder.ToString();
		}

#if UNITY_EDITOR

		private void OnValidate()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode || textComponent == null)
			{
				return;
			}

			I18n.LoadLanguage();
			Refresh();
		}

#endif
	}
}
