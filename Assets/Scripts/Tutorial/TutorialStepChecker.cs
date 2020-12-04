using Global;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Tutorial
{
    [RequireComponent(typeof(Image))]
    public class TutorialStepChecker : MonoBehaviour
    {
        [SerializeField] private int playerIndex = -1;
        [SerializeField] private Color color;

        private Image _image;
        private Color _originalColor;
        private PlayerManager.Player _player;

        public bool IsEnabled;

        public bool IsReady { get; private set; }

        private void Start()
        {
            _player = PlayerManager.Instance.Players[playerIndex];
            SetupImage();
        }

        private void SetupImage()
        {
            if (_image != null) return;
            _image = GetComponent<Image>();
            _originalColor = _image.color;
        }

        public void Disable()
        {
            _image.DOKill();

            IsEnabled = false;
            IsReady = false;
            SetupImage();
        }

        public void Enable()
        {
            _image.DOKill();
            IsEnabled = true;
        }

        private void Update()
        {
            if (!IsEnabled)
            {
                _image.color = _originalColor;
                _image.transform.localScale = Vector3.zero;
            } else if (!IsReady)
            {
                _image.transform.localScale = Vector3.one;
            }

            if (IsEnabled && _player.GameController.InteractThisFrame())
            {
                IsReady = true;
                _image.DOColor(color, 0.2f);

                Sequence seq = DOTween.Sequence();
                seq.Append(_image.transform.DOScale(1.1f, 0.1f));
                seq.Append(_image.transform.DOScale(1, 0.1f));
            }
        }
    }
}