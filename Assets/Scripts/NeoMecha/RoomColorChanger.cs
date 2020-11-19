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

        public void ChangeColor(int index)
        {
            Color color = default;
            switch(index)
            {
                case 0:
                    color = Color.white;
                    break;
                case 1:
                    color = Color.red;
                    break;
                case 2:
                    color = Color.cyan;
                    break;
                case 3:
                    color = Color.yellow;
                    break;
            }
            m_spriteRenderer.DOColor(color, 0.2f);
        }
    }
}
