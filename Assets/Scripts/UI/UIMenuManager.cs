using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject titleWrapper;

        public void OnClickPlay()
        {
            SceneManager.LoadScene(3);
        }

        public void OnClickQuit()
        {
            Application.Quit();
        }
    }
}
