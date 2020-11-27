using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField]
        private MechaLights leftMechaLights;
        [SerializeField]
        private MechaLights rightMechaLights;


    }

    [Serializable]
    public struct MechaLights
    {
        public Light2D AttLight;
        public Light2D DefLight;
        public Light2D JamLight;
        public Light2D MunLight;
        public Light2D EneLight;
        public Light2D FixLight;
    }
}
