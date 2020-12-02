using UnityEngine;

namespace NeoMecha.ConsoleControls.ButtonControl
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class ButtonTarget : ConsoleTarget
    {
        [Header("Button Look")] 
        [SerializeField] private Color focusColor;
        [SerializeField] private Color inactiveColor;
        
        private SpriteRenderer _renderer;
        private Color _originalColor;
        
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _originalColor = _renderer.color;
        }
        
        public override void Validate()
        {
            OnValidate.Invoke();
        }

        public override void Activate()
        {
            IsActive = true;
            _renderer.color = _originalColor;
        }

        public override void Desactivate()
        {
            IsActive = false;
            _renderer.color = inactiveColor;
        }

        public void Focus()
        {
            _renderer.color = focusColor;
        }

        public void Blur()
        {
            _renderer.color = _originalColor;
        }
    }
}