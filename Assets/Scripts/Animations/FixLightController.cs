using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animations
{
    class FixLightController : LightController
    {
        private bool m_isFixing;
        private float m_defaultIntensity;

        protected override void InitialState()
        {
            m_defaultIntensity = m_light.intensity;
            m_currentColor = green;
            SwitchOn(0);
        }

        public void OnDamage()
        {
            // If fixing the room, since we fix it no matter if it's damaged in between, better not show it
            if (m_isFixing) return;
            m_currentColor = red;
            m_light.intensity = m_defaultIntensity * 2;
            SwitchOn();
            StopBlink();
        }

        public void OnStartFix()
        {
            m_isFixing = true;
            m_light.intensity = m_defaultIntensity;
            m_currentColor = yellow;
            StartBlink();
        }

        public void OnFixed()
        {
            m_isFixing = false;
            m_light.intensity = m_defaultIntensity;
            m_currentColor = green;
            StopBlink();
        }
    }
}
