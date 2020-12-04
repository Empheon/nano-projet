using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Animations
{
    public class RoomSpriteFader : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer normalRoom;
        [SerializeField]
        private SpriteRenderer damagedRoom;

        private Color m_transparent = new Color(1, 1, 1, 0);

        private void Start()
        {
            damagedRoom.color = m_transparent;
        }

        public void OnFix(float duration)
        {
            normalRoom.DOColor(Color.white, duration);
            damagedRoom.DOColor(m_transparent, duration);
        }

        public void OnDamage(float duration)
        {
            damagedRoom.DOColor(Color.white, duration);
            normalRoom.DOColor(m_transparent, duration);
        }
    }
}
