using UnityEngine;
using YorfLib;

namespace Animations
{
    public class TipRandomizer : MonoBehaviour
    {
        [SerializeField]
        private int tipsNumber;

        private TranslateTMP translateTMP;

        private void Start()
        {
            translateTMP = GetComponent<TranslateTMP>();
        }

        public void RandomizeTip()
        {
            translateTMP.Text = "<i18n=\"tip_" + Random.Range(1, tipsNumber + 1) + "\">";
            translateTMP.Refresh();
        }
    }
}
