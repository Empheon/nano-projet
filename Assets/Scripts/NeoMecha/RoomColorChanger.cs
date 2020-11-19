using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

namespace NeoMecha
{
    public class RoomColorChanger : MonoBehaviour
    {
        [SerializeField]
        private Light2D jamLight;
        [SerializeField]
        private Light2D dmgLight;
        [SerializeField]
        private Light2D defLight;

        private void Start()
        {
            jamLight.color = Color.black;
            dmgLight.color = Color.black;
            defLight.color = Color.black;
        }

        public void JamOn()
        {
            DOTween.To(() => jamLight.color, (x) => jamLight.color = x, Color.yellow, 0.2f);
        }

        public void JamOff()
        {
            DOTween.To(() => jamLight.color, (x) => jamLight.color = x, Color.black, 0.2f);
        }

        public void DmgOn()
        {
            DOTween.To(() => dmgLight.color, (x) => dmgLight.color = x, Color.red, 0.2f);
        }

        public void DmgOff()
        {
            DOTween.To(() => dmgLight.color, (x) => dmgLight.color = x, Color.black, 0.2f);
        }

        public void DefOn()
        {
            DOTween.To(() => defLight.color, (x) => defLight.color = x, Color.cyan, 0.2f);
        }

        public void DefOff()
        {
            DOTween.To(() => defLight.color, (x) => defLight.color = x, Color.black, 0.2f);
        }
    }
}
