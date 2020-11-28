using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace YorfLib
{
	public class I18n
	{
		private static Dictionary<String, String> s_fields;
		public static string CurrentLang { get; private set; }

		static I18n()
		{
			LoadLanguage();
		}

		public static string GetString(string key)
		{
			string value;
			if (!s_fields.TryGetValue(key, out value))
			{
				return $"{{{key}}}";
			}

			return value;
		}

		public static string GetString(string key, params object[] values)
		{
			return string.Format(GetString(key), values);
		}

		public static void LoadLanguage()
		{
			LoadLanguage(LanguageISOCode(Application.systemLanguage));
		}

		public static void LoadLanguage(string lang, bool isFallback = false)
		{
			if (s_fields == null)
			{
				s_fields = new Dictionary<string, string>();
			}

			s_fields.Clear();

#if UNITY_EDITOR
			//if (EditorApplication.isPlayingOrWillChangePlaymode && !isFallback)
			//{
			//	lang = LanguageISOCode((SystemLanguage) EditorPrefs.GetInt("i18n_lang", (int) SystemLanguage.English));
			//}
			//else if (!EditorApplication.isPlayingOrWillChangePlaymode && !isFallback)
			//{
			//	lang = "en";
			//}
#endif

			CurrentLang = lang;

			TextAsset textAsset = Resources.Load<TextAsset>(@"I18n/" + lang);
			if (textAsset == null && lang != "en")
			{
				LoadLanguage("en", true);
				return;
			}

			if (textAsset == null)
			{
				Debug.LogError("No default lang file");
				return;
			}

			string allTexts = textAsset.text;
			if (allTexts == "")
			{
				allTexts = System.Text.Encoding.UTF8.GetString((textAsset as TextAsset).bytes);
			}

			string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			string key, value;
			for (int i = 0; i < lines.Length; i++)
			{
				int separatorIndex = lines[i].IndexOf(";");
				if (!lines[i].StartsWith("#") && separatorIndex >= 0)
				{
					key = lines[i].Substring(0, separatorIndex);
					value = lines[i].Substring(separatorIndex + 1, lines[i].Length - separatorIndex - 1).Replace("\\n", Environment.NewLine);
					s_fields.Add(key, value);
				}
			}
		}

		public static string LanguageISOCode(SystemLanguage lang)
		{
			string res = "EN";

			switch (lang)
			{
				case SystemLanguage.Afrikaans:
					res = "AF";
					break;

				case SystemLanguage.Arabic:
					res = "AR";
					break;

				case SystemLanguage.Basque:
					res = "EU";
					break;

				case SystemLanguage.Belarusian:
					res = "BY";
					break;

				case SystemLanguage.Bulgarian:
					res = "BG";
					break;

				case SystemLanguage.Catalan:
					res = "CA";
					break;

				case SystemLanguage.Chinese:
					res = "ZH";
					break;

				case SystemLanguage.ChineseSimplified:
					res = "ZH";
					break;

				case SystemLanguage.ChineseTraditional:
					res = "ZH";
					break;

				case SystemLanguage.Czech:
					res = "CS";
					break;

				case SystemLanguage.Danish:
					res = "DA";
					break;

				case SystemLanguage.Dutch:
					res = "NL";
					break;

				case SystemLanguage.English:
					res = "EN";
					break;

				case SystemLanguage.Estonian:
					res = "ET";
					break;

				case SystemLanguage.Faroese:
					res = "FO";
					break;

				case SystemLanguage.Finnish:
					res = "FI";
					break;

				case SystemLanguage.French:
					res = "FR";
					break;

				case SystemLanguage.German:
					res = "DE";
					break;

				case SystemLanguage.Greek:
					res = "EL";
					break;

				case SystemLanguage.Hebrew:
					res = "IW";
					break;

				case SystemLanguage.Hungarian:
					res = "HU";
					break;

				case SystemLanguage.Icelandic:
					res = "IS";
					break;

				case SystemLanguage.Indonesian:
					res = "IN";
					break;

				case SystemLanguage.Italian:
					res = "IT";
					break;

				case SystemLanguage.Japanese:
					res = "JA";
					break;

				case SystemLanguage.Korean:
					res = "KO";
					break;

				case SystemLanguage.Latvian:
					res = "LV";
					break;

				case SystemLanguage.Lithuanian:
					res = "LT";
					break;

				case SystemLanguage.Norwegian:
					res = "NO";
					break;

				case SystemLanguage.Polish:
					res = "PL";
					break;

				case SystemLanguage.Portuguese:
					res = "PT";
					break;

				case SystemLanguage.Romanian:
					res = "RO";
					break;

				case SystemLanguage.Russian:
					res = "RU";
					break;

				case SystemLanguage.SerboCroatian:
					res = "SH";
					break;

				case SystemLanguage.Slovak:
					res = "SK";
					break;

				case SystemLanguage.Slovenian:
					res = "SL";
					break;

				case SystemLanguage.Spanish:
					res = "ES";
					break;

				case SystemLanguage.Swedish:
					res = "SV";
					break;

				case SystemLanguage.Thai:
					res = "TH";
					break;

				case SystemLanguage.Turkish:
					res = "TR";
					break;

				case SystemLanguage.Ukrainian:
					res = "UK";
					break;

				case SystemLanguage.Unknown:
					res = "EN";
					break;

				case SystemLanguage.Vietnamese:
					res = "VI";
					break;
			}

			return res.ToLower();
		}
	}
}
