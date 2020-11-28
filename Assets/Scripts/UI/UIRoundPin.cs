using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class UIRoundPin : MonoBehaviour
    {
        [SerializeField]
        private Sprite fillerSprite;

        public void DisplayFiller()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutCirc));
            seq.Append(transform.DOScale(1, 0.5f).SetEase(Ease.InCirc));

            GetComponent<Image>().DOColor(new Color(195 / 255f, 50 / 255f, 50 / 255f), 0.5f);
        }
    }
}
