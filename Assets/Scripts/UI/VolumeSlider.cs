using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Global;
using static UnityEngine.UI.Slider;

namespace UI
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI number;
        [SerializeField]
        private Slider slider;

        private void Start()
        {
            slider.value = Settings.Instance.Volume;
            slider.onValueChanged.AddListener((x) => Settings.Instance.SetVolume(x));
        }

        private void Update()
        {
            UpdateText(Settings.Instance.Volume);
        }

        private void UpdateText(float v)
        {
            number.text = Mathf.FloorToInt((v * 10) + 0.1f).ToString();
        }
    }
}
