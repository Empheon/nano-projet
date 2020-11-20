using PathCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Animations
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField]
        private float duration;
        [SerializeField]
        private Ease easing = Ease.Linear;

        private PathCreator m_pathCreator;
        private float m_distanceTravelled;

        public void Init(PathCreator pathCreator)
        {
            m_pathCreator = pathCreator;
            DOTween.To(() => m_distanceTravelled, (x) => m_distanceTravelled = x, 1, duration).SetEase(easing);
            UpdatePositionAndRotation();
        }

        private void Update()
        {
            if (m_pathCreator == null) return;

            UpdatePositionAndRotation();
        }

        private void UpdatePositionAndRotation()
        {
            transform.position = m_pathCreator.path.GetPointAtDistance(m_distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = m_pathCreator.path.GetRotationAtDistance(m_distanceTravelled, EndOfPathInstruction.Stop);
        }
    }
}
