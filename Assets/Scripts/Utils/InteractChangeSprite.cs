using UnityEngine;

namespace Utils
{
    public class InteractChangeSprite : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private SpriteRenderer renderer;
    
        private Sprite _originalSprite;

        private void Start()
        {
            _originalSprite = renderer.sprite;
        }

        private void OnCharacterFocus(GameObject character)
        {
            renderer.sprite = sprite;
        }

        private void OnCharacterBlur(GameObject character)
        {
            renderer.sprite = _originalSprite;
        }
    }
}