using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class RoomLightController : LightController
    {
        protected override void InitialState()
        {
            m_light.color = Color.white;
        }
    }
}
