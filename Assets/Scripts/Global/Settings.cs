using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YorfLib;

namespace Global
{
    public class Settings : MonoBehaviour
    {
        [HideInInspector]
        public string LanguageKey;
        [HideInInspector]
        public float Volume;

        public static Settings Instance;

        private void Awake()
        {
            Instance = this;
            Load();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        public void Load()
        {
            SetVolume(PlayerPrefs.GetFloat("Volume", 0.8f));
            SetLanguage(PlayerPrefs.GetString("LanguageKey", "en"));
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("Volume", Volume);
            PlayerPrefs.SetString("LanguageKey", LanguageKey);
            PlayerPrefs.Save();
        }

        public void SetLanguage(string languageKey)
        {
            LanguageKey = languageKey;
            I18n.LoadLanguage(LanguageKey);
        }

        public void SetVolume(float volume)
        {
            Volume = volume;
            AkSoundEngine.SetRTPCValue("Master_Control_Volume", volume * 100);
        }
    }
}
