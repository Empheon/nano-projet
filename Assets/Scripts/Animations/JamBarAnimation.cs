using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class JamBarAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject outsideEmissive;
        [SerializeField]
        private GameObject jaugeMask;

        [SerializeField]
        private float offset = 0.64f;

        private void Start()
        {
            OnLoad(0);
        }

        public void OnLoad(float duration)
        {
            jaugeMask.transform.DOKill();
            jaugeMask.transform.DOLocalMoveX(offset, duration).SetEase(Ease.Linear).onComplete = () => outsideEmissive.SetActive(true);
        }

        public void OnUnload()
        {
            jaugeMask.transform.DOKill();
            outsideEmissive.SetActive(false);
            jaugeMask.transform.DOLocalMoveX(0, 1).SetEase(Ease.OutExpo);
        }
    }
}
