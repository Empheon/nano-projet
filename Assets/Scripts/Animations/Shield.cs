using NeoMecha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Animations
{
    public class Shield : MonoBehaviour
    {
        [SerializeField]
        private float protectionDuration;

        private Animator m_animator;
        private Room m_roomToProtect;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        public void Protect(Room room, Action callback)
        {
            StartCoroutine(ProtectAnim(room, callback));
        }

        private IEnumerator ProtectAnim(Room room, Action callback)
        {
            transform.DOMoveY(room.transform.position.y, 0.3f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(0.3f);

            m_animator.SetTrigger("OpenShield");

            yield return new WaitForSeconds(protectionDuration);

            m_animator.SetTrigger("CloseShield");
            callback();
        }
    }
}
