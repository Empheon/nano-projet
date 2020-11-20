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
        private float rotationNumbers;
        [SerializeField]
        private Ease easing = Ease.Linear;

        [SerializeField]
        private AnimationCurve animationCurve;

        private PathCreator m_pathCreator;
        private float m_timeOnPath;
        private float m_normalAngleOnPath;

        public void Init(PathCreator pathCreator, Action callback)
        {
            m_pathCreator = pathCreator;
            DOTween.To(() => m_timeOnPath, (x) => m_timeOnPath = x, 1, duration).SetEase(animationCurve).OnComplete(() => {
                callback();
                Destroy(gameObject);
            });

            DOTween.To(() => m_normalAngleOnPath, (x) => m_normalAngleOnPath = x, 360 * rotationNumbers, duration).SetEase(animationCurve);
            UpdatePositionAndRotation();
        }

        private void Update()
        {
            if (m_pathCreator == null) return;

            UpdatePositionAndRotation();
        }

        private void UpdatePositionAndRotation()
        {
            m_pathCreator.bezierPath.GlobalNormalsAngle = m_normalAngleOnPath % 360;
            m_pathCreator.EditorData.VertexPathSettingsChanged();

            var newPos = m_pathCreator.path.GetPointAtTime(m_timeOnPath, EndOfPathInstruction.Stop);
            newPos.z = transform.position.z;
            transform.position = newPos;
            transform.rotation = m_pathCreator.path.GetRotation(m_timeOnPath, EndOfPathInstruction.Stop);
        }
    }
}
