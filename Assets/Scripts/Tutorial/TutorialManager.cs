using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using YorfLib;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField]
        private int menuSceneIndex;

        [SerializeField]
        private MechaLights leftMechaLights;
        [SerializeField]
        private MechaLights rightMechaLights;

        [SerializeField]
        private Light2D globalLight;

        [SerializeField]
        private TextMeshProUGUI mainText;
        [SerializeField]
        private TextMeshProUGUI subText;

        [SerializeField]
        private float stepDuration = 5;
        [SerializeField]
        private float fastStepDuration = 2;

        [SerializeField]
        private Color defaultColor = Color.white;
        [SerializeField]
        private Color redColor = Color.red;

        [SerializeField]
        private List<PressurePlate> attPlates;
        [SerializeField]
        private List<PressurePlate> defPlates;
        [SerializeField]
        private List<PressurePlate> jamPlates;

        private int m_attButtonPressed;
        private int m_defButtonPressed;
        private int m_jamButtonPressed;

        private Coroutine m_coroutine;

        private void Start()
        {
            leftMechaLights.AttLight.SwitchOff(0);
            leftMechaLights.DefLight.SwitchOff(0);
            leftMechaLights.JamLight.SwitchOff(0);
            leftMechaLights.EneLight.SwitchOff(0);
            leftMechaLights.MunLight.SwitchOff(0);
            leftMechaLights.FixLight.SwitchOff(0);

            rightMechaLights.AttLight.SwitchOff(0);
            rightMechaLights.DefLight.SwitchOff(0);
            rightMechaLights.JamLight.SwitchOff(0);
            rightMechaLights.EneLight.SwitchOff(0);
            rightMechaLights.MunLight.SwitchOff(0);
            rightMechaLights.FixLight.SwitchOff(0);

            mainText.text = "";
            subText.text = "";

            m_coroutine = StartCoroutine(Tuto());
        }

        private void OnDestroy()
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
            }
        }

        private IEnumerator Tuto()
        {
            mainText.text = I18n.GetString("tuto_main_1");
            yield return new WaitForSeconds(stepDuration / 2);
            mainText.text = I18n.GetString("tuto_main_2");
            subText.text = I18n.GetString("tuto_sub_2");
            leftMechaLights.AttLight.SwitchOn();
            rightMechaLights.AttLight.SwitchOn();

            foreach(PressurePlate pp in attPlates)
            {
                pp.Show();
            }
            while (m_attButtonPressed < 2)
            {
                yield return new WaitForSeconds(0.1f);
            }

            subText.text = "";
            mainText.text = I18n.GetString("tuto_main_3");
            leftMechaLights.MunLight.StartBlink();
            rightMechaLights.MunLight.StartBlink();
            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_4");
            leftMechaLights.MunLight.StopBlink();
            rightMechaLights.MunLight.StopBlink();
            leftMechaLights.AttLight.StartBlink();
            leftMechaLights.DefLight.StartBlink();
            leftMechaLights.JamLight.StartBlink();
            rightMechaLights.AttLight.StartBlink();
            rightMechaLights.DefLight.StartBlink();
            rightMechaLights.JamLight.StartBlink();
            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_5");
            leftMechaLights.AttLight.Color = redColor;
            leftMechaLights.DefLight.Color = redColor;
            leftMechaLights.JamLight.Color = redColor;
            rightMechaLights.AttLight.Color = redColor;
            rightMechaLights.DefLight.Color = redColor;
            rightMechaLights.JamLight.Color = redColor;
            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_6");
            leftMechaLights.AttLight.StopBlink();
            leftMechaLights.DefLight.StopBlink();
            leftMechaLights.JamLight.StopBlink();
            rightMechaLights.AttLight.StopBlink();
            rightMechaLights.DefLight.StopBlink();
            rightMechaLights.JamLight.StopBlink();
            leftMechaLights.AttLight.Color = defaultColor;
            leftMechaLights.DefLight.Color = defaultColor;
            leftMechaLights.JamLight.Color = defaultColor;
            rightMechaLights.AttLight.Color = defaultColor;
            rightMechaLights.DefLight.Color = defaultColor;
            rightMechaLights.JamLight.Color = defaultColor;

            leftMechaLights.FixLight.StartBlink();
            rightMechaLights.FixLight.StartBlink();
            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_7");
            subText.text = I18n.GetString("tuto_sub_7");
            leftMechaLights.FixLight.StopBlink();
            rightMechaLights.FixLight.StopBlink();
            leftMechaLights.AttLight.SwitchOff();
            rightMechaLights.AttLight.SwitchOff();
            leftMechaLights.DefLight.SwitchOn();
            rightMechaLights.DefLight.SwitchOn();

            foreach (PressurePlate pp in attPlates)
            {
                pp.Hide();
            }
            foreach (PressurePlate pp in defPlates)
            {
                pp.Show();
            }
            while (m_defButtonPressed < 2)
            {
                yield return new WaitForSeconds(0.1f);
            }

            subText.text = "";
            mainText.text = I18n.GetString("tuto_main_8");
            leftMechaLights.EneLight.StartBlink();
            rightMechaLights.EneLight.StartBlink();
            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_9");
            leftMechaLights.EneLight.StopBlink();
            rightMechaLights.EneLight.StopBlink();
            leftMechaLights.AttLight.StartBlink();
            leftMechaLights.DefLight.StartBlink();
            leftMechaLights.JamLight.StartBlink();
            rightMechaLights.AttLight.StartBlink();
            rightMechaLights.DefLight.StartBlink();
            rightMechaLights.JamLight.StartBlink();

            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_10");
            subText.text = I18n.GetString("tuto_sub_10");
            leftMechaLights.AttLight.StopBlink();
            leftMechaLights.DefLight.StopBlink();
            leftMechaLights.JamLight.StopBlink();
            rightMechaLights.AttLight.StopBlink();
            rightMechaLights.DefLight.StopBlink();
            rightMechaLights.JamLight.StopBlink();
            leftMechaLights.DefLight.SwitchOff();
            rightMechaLights.DefLight.SwitchOff();
            leftMechaLights.JamLight.SwitchOn();
            rightMechaLights.JamLight.SwitchOn();

            foreach (PressurePlate pp in defPlates)
            {
                pp.Hide();
            }
            foreach (PressurePlate pp in jamPlates)
            {
                pp.Show();
            }
            while (m_jamButtonPressed < 2)
            {
                yield return new WaitForSeconds(0.1f);
            }

            subText.text = "";
            mainText.text = I18n.GetString("tuto_main_11");
            leftMechaLights.AttLight.StartBlink();
            leftMechaLights.DefLight.StartBlink();
            leftMechaLights.JamLight.StartBlink();
            rightMechaLights.AttLight.StartBlink();
            rightMechaLights.DefLight.StartBlink();
            rightMechaLights.JamLight.StartBlink();
            yield return new WaitForSeconds(stepDuration);

            mainText.text = I18n.GetString("tuto_main_12");
            leftMechaLights.AttLight.StopBlink();
            leftMechaLights.DefLight.StopBlink();
            rightMechaLights.AttLight.StopBlink();
            rightMechaLights.DefLight.StopBlink();

            yield return new WaitForSeconds(stepDuration);
            leftMechaLights.JamLight.SwitchOff();
            rightMechaLights.JamLight.SwitchOff();
            leftMechaLights.JamLight.StopBlink();
            rightMechaLights.JamLight.StopBlink();

            DOTween.To(() => globalLight.intensity, (x) => globalLight.intensity = x, 1, 0.5f);

            foreach (PressurePlate pp in jamPlates)
            {
                pp.Hide();
            }

            mainText.text = I18n.GetString("tuto_main_13");
            yield return new WaitForSeconds(fastStepDuration);
            mainText.text = I18n.GetString("tuto_main_14");
            yield return new WaitForSeconds(fastStepDuration);
            mainText.text = I18n.GetString("tuto_main_15");
            yield return new WaitForSeconds(stepDuration / 2);
            SceneManager.LoadScene(menuSceneIndex);
        }

        public void OnAttButtonPressed()
        {
            m_attButtonPressed++;
        }

        public void OnDefButtonPressed()
        {
            m_defButtonPressed++;
        }

        public void OnJamButtonPressed()
        {
            m_jamButtonPressed++;
        }

    }

    [Serializable]
    public struct MechaLights
    {
        public LightController AttLight;
        public LightController DefLight;
        public LightController JamLight;
        public LightController MunLight;
        public LightController EneLight;
        public LightController FixLight;
    }
}
