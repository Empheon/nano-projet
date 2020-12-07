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
            OnUnload();
        }

        public void OnLoad()
        {
            m_currentColor = green;
            SwitchOn();
        }

        public void OnUnload()
        {
            m_currentColor = red;
            SwitchOn();
        }

    }
}
