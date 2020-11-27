using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public class UIRoundPin : MonoBehaviour
    {
        [SerializeField]
        private GameObject fillerObject;

        public void DisplayFiller(bool b)
        {
            fillerObject.SetActive(b);
        }
    }
}
