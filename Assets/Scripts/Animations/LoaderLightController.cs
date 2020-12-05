using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animations
{
    class LoaderLightController : LightController
    {

        protected override void InitialState()
        {
            m_currentColor = green;
            SwitchOff(0);
        }

        public void OnLoad()
        {
            SwitchOn();
        }

        public void OnUnload()
        {
            SwitchOff();
        }

    }
}
