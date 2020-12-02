using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Tutorial
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField]
        private Image background;
        [SerializeField]
        private TextMeshProUGUI text;


        private void OnEnable()
        {
            text.color = new Color(0, 0, 0, 0);

            Sequence seq = DOTween.Sequence();
            seq.Append(background.DOColor(Color.black, 1).From(new Color(0, 0, 0, 0)));
            seq.Append(text.DOColor(Color.white, 0.5f));

        }
    }
}
