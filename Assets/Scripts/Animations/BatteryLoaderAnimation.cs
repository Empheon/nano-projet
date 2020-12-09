using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class BatteryLoaderAnimation : MonoBehaviour
    {
        [SerializeField]
        private Sprite loadedSprite;
        [SerializeField]
        private Sprite unloadedSprite;

        private SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnLoad()
        {
            m_spriteRenderer.sprite = loadedSprite;
        }

        public void OnUnload()
        {
            m_spriteRenderer.sprite = unloadedSprite;
        }
    }
}
