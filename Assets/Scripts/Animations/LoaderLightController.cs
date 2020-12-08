using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

namespace Animations
{
    class LoaderLightController : LightController
    {
        [SerializeField]
        private Light2D gyrophare;
        [SerializeField]
        private RoomLightController roomLightController;

        [SerializeField]
        private float rotationSpeed = 1;

        [SerializeField]
        private bool isJamRoomLight = false;

        private float m_gyroIntensity;

        private bool m_isLoaded;
        private bool m_isBroken;

        protected override void InitialState()
        {
            gyrophare.gameObject.SetActive(true);
            m_gyroIntensity = gyrophare.intensity;
            gyrophare.intensity = 0;

            if (isJamRoomLight)
            {
                m_light.gameObject.SetActive(false);
                SwitchOff(0);
            } else
            {
                OnUnload();
            }
        }

        public void OnLoad()
        {
            m_isLoaded = true;
            m_currentColor = green;

            if (!m_isBroken)
            {
                SwitchOn();
            }
        }

        public void OnUnload()
        {
            m_isLoaded = false;
            m_currentColor = red;

            if (!m_isBroken)
            {
                SwitchOn();
            }
        }

        public void OnBreak()
        {
            if (!isJamRoomLight) m_light.gameObject.SetActive(false);

            roomLightController.SwitchOff();
            m_isBroken = true;
            m_currentColor = red;
            SwitchOn();
            DOTween.To(() => gyrophare.intensity, (x) => gyrophare.intensity = x, m_gyroIntensity, 1);
        }

        public void OnUnbreak()
        {
            DOTween.To(() => gyrophare.intensity, (x) => gyrophare.intensity = x, 0, 1).onComplete = () => {
                m_isBroken = false;
                roomLightController.SwitchOn();

                if (!isJamRoomLight)
                {
                    m_light.gameObject.SetActive(true);
                    if (m_isLoaded) OnLoad(); else OnUnload();
                } else
                {
                    SwitchOff();
                }
            };
        }

        public void OnJam()
        {
            if (m_isBroken) return;
            roomLightController.SwitchOff();
            SwitchOff();
        }

        public void OnUnJam()
        {
            if (m_isBroken) return;
            roomLightController.SwitchOn();

            if (!isJamRoomLight)
            {
                SwitchOn();
            }
        }

        private void Update()
        {
            gyrophare.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }

    }
}
