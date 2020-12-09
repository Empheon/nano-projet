using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private float time = 180;

        public UnityEvent OnTimeout;

        private TextMeshProUGUI m_text;

        private void Start()
        {
            m_text = GetComponent<TextMeshProUGUI>();
            UpdateText(time);
        }

        private void Update()
        {
            time -= Time.deltaTime;
            UpdateText(time);

            if (time <= 0f)
            {
                OnTimeout.Invoke();
                Destroy(this);
            }
        }

        private void UpdateText(float v)
        {
            m_text.text = Mathf.Max(0, Mathf.FloorToInt(v)).ToString();
        }
    }
}
