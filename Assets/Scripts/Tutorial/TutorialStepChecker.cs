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
        [SerializeField] private Image image;

        private Color _originalColor;
        private PlayerManager.Player _player;

        public bool IsEnabled;

        public bool IsReady { get; private set; }

        private void Start()
        {
            _player = PlayerManager.Instance.Players[playerIndex];
            _originalColor = image.color;
        }

        public void Disable()
        {
            image.DOKill();

            IsEnabled = false;
            IsReady = false;
        }

        public void Enable()
        {
            image.DOKill();
            IsEnabled = true;
        }

        private void Update()
        {
            if (!IsEnabled)
            {
                image.color = _originalColor;
                transform.localScale = Vector3.zero;
            } else if (!IsReady)
            {
                transform.localScale = Vector3.one;
            }

            if (IsEnabled && _player.GameController.InteractThisFrame())
            {
                IsReady = true;
                image.DOColor(color, 0.2f);

                Sequence seq = DOTween.Sequence();
                seq.Append(image.transform.DOScale(1.1f, 0.1f));
                seq.Append(image.transform.DOScale(1, 0.1f));
            }
        }
    }
}