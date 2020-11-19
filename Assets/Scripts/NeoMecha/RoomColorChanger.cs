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
        private Light2D dmgDefLight;

        private void Start()
        {
            jamLight.color = Color.black;
            dmgDefLight.color = Color.green;
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
            DOTween.To(() => dmgDefLight.color, (x) => dmgDefLight.color = x, Color.red, 0.2f);
        }

        public void DefOn()
        {
            DOTween.To(() => dmgDefLight.color, (x) => dmgDefLight.color = x, Color.cyan, 0.2f);
        }

        public void DmgOrDefOff()
        {
            DOTween.To(() => dmgDefLight.color, (x) => dmgDefLight.color = x, Color.green, 0.2f);
        }
    }
}
