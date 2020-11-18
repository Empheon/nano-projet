using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace NeoMecha
{
    public class RoomColorChanger : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeColor(Color color)
        {
            m_spriteRenderer.DOColor(color, 0.2f);
        }
    }
}
