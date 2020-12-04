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
        
        public bool IsReady { get; private set; }

        private void Start()
        {
            _player = PlayerManager.Instance.Players[playerIndex];
            _image = GetComponent<Image>();
            _originalColor = _image.color;
        }

        private void OnDisable()
        {
            IsReady = false;
            _image.color = _originalColor;
        }

        private void Update()
        {
            if (_player.GameController.InteractThisFrame())
            {
                IsReady = true;
                _image.DOColor(color, 0.2f);
            }
        }
    }
}