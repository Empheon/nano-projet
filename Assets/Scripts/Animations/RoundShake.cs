using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class RoundShake : MonoBehaviour
    {
        public void Shake(float trauma)
        {
            CameraShake.Instance.AddTrauma(trauma);
        }
    }
}
