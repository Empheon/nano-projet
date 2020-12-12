using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Menu
{
    public class BackButton : MonoBehaviour
    {
        public UnityEvent OnBackPressed;

        private void Update()
        {
            if (WasBackPressed())
            {
                OnBackPressed.Invoke();
                return;
            }
        }

        private bool WasBackPressed()
        {
            if (Gamepad.all.Any((g) => g.buttonEast.wasPressedThisFrame)
                || InputSystem.devices.Any((g) => {
                    if (g is Keyboard keyboard)
                    {
                        return keyboard.escapeKey.wasPressedThisFrame;
                    }

                    return false;
                }))
            {
                return true;
            }
            return false;
        }
    }
}
