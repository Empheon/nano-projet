using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace NeoMecha
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ConsolePanelButton : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnFocus;
        public UnityEvent OnBlur;
        public UnityEvent OnValidate;
        public UnityEvent OnActivate;
        public UnityEvent OnDesactivate;

        [Header("Button Look")] 
        [SerializeField] private Color baseColor;
        [SerializeField] private Color focusColor;
        [SerializeField] private Color validatedColor;
        [SerializeField] private float validateTime = 0.05f;

        private bool _isFocused = false;
        private bool _isActive = true;
        private SpriteRenderer _renderer;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if(_isActive == value) return;
                
                if (value)
                {
                    OnActivate.Invoke();
                }
                else
                {
                    OnDesactivate.Invoke();
                }

                _isActive = value;
            }
        }
        
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.color = baseColor;
        }

        public void Focus()
        {
            if (!_isFocused)
            {
                _renderer.color = focusColor;
                _isFocused = true;
                OnFocus.Invoke();
            }
        }

        public void Blur()
        {
            if (_isFocused)
            {
                _renderer.color = baseColor;
                _isFocused = false;
                OnBlur.Invoke();
            }
        }

        public void Validate()
        {
            StartCoroutine(DoValidate());
        }

        private IEnumerator DoValidate()
        {
            _renderer.color = validatedColor;
            
            yield return new WaitForSeconds(validateTime);
            
            _renderer.color = baseColor;
            OnValidate.Invoke();
        }
        
    }
}