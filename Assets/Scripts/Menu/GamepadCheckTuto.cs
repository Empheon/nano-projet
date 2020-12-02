using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class GamepadCheckTuto : GamepadCheck
    {
        protected override void Disconnected()
        {
            IsConnected = false;
            indicatorObject.SetActive(false);
        }

        protected override void Connected()
        {
            IsConnected = true;
            indicatorObject.SetActive(true);
        }

        protected override void Setup()
        {
            indicatorObject.SetActive(false);
            _indicTransform = indicatorObject.GetComponent<Transform>();
            _indicRenderer = indicatorObject.GetComponent<Image>();
            _baseColor = _indicRenderer.color;
        }
    }
}
