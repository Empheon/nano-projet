using UnityEngine;

namespace NeoMecha.ConsoleControls.ButtonControl
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class ButtonTarget : ConsoleTarget
    {
        [Header("Button Look")] 
        [SerializeField] private Sprite focusSprite;
        [SerializeField] private Sprite inactiveSprite;
        
        private SpriteRenderer _renderer;
        
        private void OnEnable()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        
        public override void Validate() { }

        public override void Activate()
        {
            IsActive = true;
            _renderer.enabled = true;
        }

        public override void Desactivate()
        {
            IsActive = false;
            _renderer.enabled = false;
        }

        public void Focus()
        {
            _renderer.sprite = focusSprite;
        }

        public void Blur()
        {
            _renderer.sprite = inactiveSprite;
        }
    }
}