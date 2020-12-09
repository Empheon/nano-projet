using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Animations
{
    public class FixBarAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject jaugeMask;
        [SerializeField]
        private float duration;

        private bool m_isFixing;

        private void Start()
        {
            var pos = jaugeMask.transform.localPosition;
            pos.x = jaugeMask.transform.localScale.x + 0.01f;
            jaugeMask.transform.localPosition = pos;
        }

        public void OnStartFix()
        {
            if (m_isFixing) return;
            m_isFixing = true;
            jaugeMask.transform.DOLocalMoveX(jaugeMask.transform.localScale.x + 0.01f, duration).SetEase(Ease.Linear).onComplete = () => m_isFixing = false;
        }

        public void OnDamage()
        {
            jaugeMask.transform.DOLocalMoveX(0, 1).SetEase(Ease.OutExpo);
        }

    }
}
