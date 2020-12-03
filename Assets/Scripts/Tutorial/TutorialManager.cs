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
using Menu;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField]
        private int menuSceneIndex;

        [SerializeField]
        private MechaInfo leftMechaInfo;
        [SerializeField]
        private MechaInfo rightMechaInfo;

        [SerializeField]
        private Light2D globalLight;

        [SerializeField]
        private TextMeshProUGUI mainText;
        [SerializeField]
        private TextMeshProUGUI subText;

        [SerializeField]
        private GamepadCheck leftActivationButton;
        [SerializeField]
        private GamepadCheck rightActivationButton;

        [SerializeField]
        private GameObject leftMecha;
        [SerializeField]
        private GameObject rightMecha;

        [SerializeField]
        private GameObject endPanel;

        //[SerializeField]
        //private float stepDuration = 5;
        //[SerializeField]
        //private float fastStepDuration = 2;

        [SerializeField]
        private Color defaultColor = Color.white;
        [SerializeField]
        private Color redColor = Color.red;

        //[SerializeField]
        //private List<PressurePlate> attPlates;
        //[SerializeField]
        //private List<PressurePlate> defPlates;
        //[SerializeField]
        //private List<PressurePlate> jamPlates;

        //private int m_attButtonPressed;
        //private int m_defButtonPressed;
        //private int m_jamButtonPressed;
        private int m_confirmButtonPressed;

        private Coroutine m_coroutine;

        private GamepadCheck[] m_checkers;

        private void Start()
        {
            m_checkers = new GamepadCheck[2];
            m_checkers[0] = leftActivationButton;
            m_checkers[1] = rightActivationButton;

            endPanel.SetActive(false);

            foreach (ButtonActivator buttonActivator in leftMecha.GetComponentsInChildren<ButtonActivator>())
            {
                buttonActivator.Init(leftActivationButton.gameObject);
            }

            foreach (ButtonActivator buttonActivator in rightMecha.GetComponentsInChildren<ButtonActivator>())
            {
                buttonActivator.Init(rightActivationButton.gameObject);
            }

            DisableButtons();
            DisableAllColliders();

            leftMechaInfo.AttLight.SwitchOff(0);
            leftMechaInfo.DefLight.SwitchOff(0);
            leftMechaInfo.JamLight.SwitchOff(0);
            leftMechaInfo.EneLight.SwitchOff(0);
            leftMechaInfo.MunLight.SwitchOff(0);
            leftMechaInfo.FixLight.SwitchOff(0);

            rightMechaInfo.AttLight.SwitchOff(0);
            rightMechaInfo.DefLight.SwitchOff(0);
            rightMechaInfo.JamLight.SwitchOff(0);
            rightMechaInfo.EneLight.SwitchOff(0);
            rightMechaInfo.MunLight.SwitchOff(0);
            rightMechaInfo.FixLight.SwitchOff(0);

            mainText.text = "";
            subText.text = "";

            m_coroutine = StartCoroutine(Tuto());
        }

        private void DisableButtons()
        {
            foreach(GamepadCheck gamepadCheck in m_checkers)
            {
                gamepadCheck.gameObject.SetActive(false);
            }
        }

        private void EnableButtons()
        {
            foreach (GamepadCheck gamepadCheck in m_checkers)
            {
                gamepadCheck.gameObject.SetActive(true);
            }
        }

        private void DisableAllColliders()
        {
            leftMechaInfo.AttCollider.gameObject.SetActive(false);
            rightMechaInfo.AttCollider.gameObject.SetActive(false);
            leftMechaInfo.DefCollider.gameObject.SetActive(false);
            rightMechaInfo.DefCollider.gameObject.SetActive(false);
            leftMechaInfo.JamCollider.gameObject.SetActive(false);
            rightMechaInfo.JamCollider.gameObject.SetActive(false);
            leftMechaInfo.MunCollider.gameObject.SetActive(false);
            rightMechaInfo.MunCollider.gameObject.SetActive(false);;
            leftMechaInfo.EneCollider.gameObject.SetActive(false);;
            rightMechaInfo.EneCollider.gameObject.SetActive(false);;
            leftMechaInfo.FixCollider.gameObject.SetActive(false);;
            rightMechaInfo.FixCollider.gameObject.SetActive(false);;
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
            yield return new WaitForSeconds(5);


            mainText.text = I18n.GetString("tuto_main_2");
            subText.text = I18n.GetString("tuto_sub_goto_att");
            leftMechaInfo.AttLight.SwitchOn();
            rightMechaInfo.AttLight.SwitchOn();

            leftMechaInfo.AttCollider.gameObject.SetActive(true);;
            rightMechaInfo.AttCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            subText.text = "";
            mainText.text = I18n.GetString("tuto_main_3");
            subText.text = I18n.GetString("tuto_sub_goto_mun");
            leftMechaInfo.MunLight.StartBlink();
            rightMechaInfo.MunLight.StartBlink();

            leftMechaInfo.MunCollider.gameObject.SetActive(true);;
            rightMechaInfo.MunCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_4");
            subText.text = I18n.GetString("tuto_sub_goto_several");
            leftMechaInfo.MunLight.StopBlink();
            rightMechaInfo.MunLight.StopBlink();
            leftMechaInfo.AttLight.StartBlink();
            leftMechaInfo.DefLight.StartBlink();
            leftMechaInfo.JamLight.StartBlink();
            rightMechaInfo.AttLight.StartBlink();
            rightMechaInfo.DefLight.StartBlink();
            rightMechaInfo.JamLight.StartBlink();

            leftMechaInfo.AttCollider.gameObject.SetActive(true);;
            rightMechaInfo.AttCollider.gameObject.SetActive(true);;
            leftMechaInfo.DefCollider.gameObject.SetActive(true);;
            rightMechaInfo.DefCollider.gameObject.SetActive(true);;
            leftMechaInfo.JamCollider.gameObject.SetActive(true);;
            rightMechaInfo.JamCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_5");
            subText.text = "";
            leftMechaInfo.AttLight.Color = redColor;
            leftMechaInfo.DefLight.Color = redColor;
            leftMechaInfo.JamLight.Color = redColor;
            rightMechaInfo.AttLight.Color = redColor;
            rightMechaInfo.DefLight.Color = redColor;
            rightMechaInfo.JamLight.Color = redColor;

            yield return new WaitForSeconds(4);
            EnableButtons();

            yield return StartCoroutine(CheckButtonsConfirmed());

            mainText.text = I18n.GetString("tuto_main_6");
            subText.text = I18n.GetString("tuto_sub_goto_fix");
            leftMechaInfo.AttLight.StopBlink();
            leftMechaInfo.DefLight.StopBlink();
            leftMechaInfo.JamLight.StopBlink();
            rightMechaInfo.AttLight.StopBlink();
            rightMechaInfo.DefLight.StopBlink();
            rightMechaInfo.JamLight.StopBlink();
            leftMechaInfo.AttLight.Color = defaultColor;
            leftMechaInfo.DefLight.Color = defaultColor;
            leftMechaInfo.JamLight.Color = defaultColor;
            rightMechaInfo.AttLight.Color = defaultColor;
            rightMechaInfo.DefLight.Color = defaultColor;
            rightMechaInfo.JamLight.Color = defaultColor;
            leftMechaInfo.AttLight.SwitchOff();
            rightMechaInfo.AttLight.SwitchOff();

            leftMechaInfo.FixLight.StartBlink();
            rightMechaInfo.FixLight.StartBlink();

            leftMechaInfo.FixCollider.gameObject.SetActive(true);;
            rightMechaInfo.FixCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_7");
            subText.text = I18n.GetString("tuto_sub_goto_def");
            leftMechaInfo.FixLight.StopBlink();
            rightMechaInfo.FixLight.StopBlink();
            leftMechaInfo.DefLight.SwitchOn();
            rightMechaInfo.DefLight.SwitchOn();

            leftMechaInfo.DefCollider.gameObject.SetActive(true);;
            rightMechaInfo.DefCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_8");
            subText.text = I18n.GetString("tuto_sub_goto_ene");
            leftMechaInfo.EneLight.StartBlink();
            rightMechaInfo.EneLight.StartBlink();

            leftMechaInfo.EneCollider.gameObject.SetActive(true);;
            rightMechaInfo.EneCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_9");
            subText.text = I18n.GetString("tuto_sub_goto_several");
            leftMechaInfo.EneLight.StopBlink();
            rightMechaInfo.EneLight.StopBlink();
            leftMechaInfo.AttLight.StartBlink();
            leftMechaInfo.DefLight.StartBlink();
            leftMechaInfo.JamLight.StartBlink();
            rightMechaInfo.AttLight.StartBlink();
            rightMechaInfo.DefLight.StartBlink();
            rightMechaInfo.JamLight.StartBlink();

            leftMechaInfo.AttCollider.gameObject.SetActive(true);;
            rightMechaInfo.AttCollider.gameObject.SetActive(true);;
            leftMechaInfo.DefCollider.gameObject.SetActive(true);;
            rightMechaInfo.DefCollider.gameObject.SetActive(true);;
            leftMechaInfo.JamCollider.gameObject.SetActive(true);;
            rightMechaInfo.JamCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_10");
            subText.text = I18n.GetString("tuto_sub_goto_jam");
            leftMechaInfo.AttLight.StopBlink();
            leftMechaInfo.DefLight.StopBlink();
            leftMechaInfo.JamLight.StopBlink();
            rightMechaInfo.AttLight.StopBlink();
            rightMechaInfo.DefLight.StopBlink();
            rightMechaInfo.JamLight.StopBlink();
            leftMechaInfo.DefLight.SwitchOff();
            rightMechaInfo.DefLight.SwitchOff();
            leftMechaInfo.JamLight.SwitchOn();
            rightMechaInfo.JamLight.SwitchOn();

            leftMechaInfo.JamCollider.gameObject.SetActive(true);;
            rightMechaInfo.JamCollider.gameObject.SetActive(true);;

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_11");
            subText.text = "";
            leftMechaInfo.AttLight.StartBlink();
            leftMechaInfo.DefLight.StartBlink();
            leftMechaInfo.JamLight.StartBlink();
            rightMechaInfo.AttLight.StartBlink();
            rightMechaInfo.DefLight.StartBlink();
            rightMechaInfo.JamLight.StartBlink();

            yield return new WaitForSeconds(4);
            EnableButtons();

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            mainText.text = I18n.GetString("tuto_main_12");
            leftMechaInfo.AttLight.StopBlink();
            leftMechaInfo.DefLight.StopBlink();
            rightMechaInfo.AttLight.StopBlink();
            rightMechaInfo.DefLight.StopBlink();

            yield return new WaitForSeconds(4);
            EnableButtons();

            yield return StartCoroutine(CheckButtonsConfirmed());
            DisableAllColliders();

            leftMechaInfo.JamLight.SwitchOff();
            rightMechaInfo.JamLight.SwitchOff();
            leftMechaInfo.JamLight.StopBlink();
            rightMechaInfo.JamLight.StopBlink();

            //DOTween.To(() => globalLight.intensity, (x) => globalLight.intensity = x, 1, 0.5f);

            endPanel.SetActive(true);

            yield return new WaitForSeconds(7);

            SceneManager.LoadScene(menuSceneIndex);
        }

        private IEnumerator CheckButtonsConfirmed()
        {
            while (!m_checkers.All(checker => checker.IsReady))
            {
                yield return new WaitForSeconds(0.1f);
            }
            DisableButtons();
        }
    }

    [Serializable]
    public struct MechaInfo
    {
        public LightController AttLight;
        public LightController DefLight;
        public LightController JamLight;
        public LightController MunLight;
        public LightController EneLight;
        public LightController FixLight;

        public ButtonActivator AttCollider;
        public ButtonActivator DefCollider;
        public ButtonActivator JamCollider;
        public ButtonActivator MunCollider;
        public ButtonActivator EneCollider;
        public ButtonActivator FixCollider;
    }
}
