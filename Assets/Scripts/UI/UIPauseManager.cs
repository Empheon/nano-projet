using Global.Loading;
using UnityEngine;

namespace UI
{
    public class UIPauseManager : MonoBehaviour
    {

        [SerializeField] private int mainMenuSceneIndex;

        public void Open()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        public void Close()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        
        public void OnClickQuit()
        {
            Application.Quit();
        }

        public void OnMainMenuClick()
        {
            Close();
            LoadingScreen.Instance.LoadScene(mainMenuSceneIndex);
        }

    }
}