using System.Collections;
using DG.Tweening;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu
{
    public class GamepadCheck : MonoBehaviour
    {
        [SerializeField] private GameObject indicatorObject;
        [SerializeField] private PlayerManager.Team team;

        [SerializeField] private Color validatedColor;

        [SerializeField] private float checkFrequency = 10;
        
        private Gamepad _gamepad;
        private Image _indicRenderer;
        private Transform _indicTransform;
        
        private Color _baseColor;

        public bool IsConnected { get; private set; } = false;
        public bool IsReady { get; private set; } = false;

        private IEnumerator Start()
        {
            indicatorObject.SetActive(false);
            _indicTransform = indicatorObject.GetComponent<Transform>();
            _indicTransform.localScale = Vector3.zero;
            _indicRenderer = indicatorObject.GetComponent<Image>();
            _baseColor = _indicRenderer.color;
            
            for (;;)
            {
                if (!IsConnected)
                {
                    foreach (var player in PlayerManager.Instance.Players)
                    {
                        if (player.team == team)
                        {
                            _gamepad = player.gamepad;
                            Connected();
                            break;
                        }
                    }
                }
                else
                {
                    var gamepadStillHere = false;
                    foreach (var player in PlayerManager.Instance.Players)
                    {
                        if (player.gamepad == _gamepad) gamepadStillHere = true;
                    }

                    if (!gamepadStillHere)
                    {
                        _gamepad = null;
                        Disconnected();
                    }
                }
            
                yield return new WaitForSeconds(1 / checkFrequency);
            }
        }

        private void Update()
        {
            if (IsConnected && _gamepad.buttonSouth.wasPressedThisFrame)
            {
                if (IsReady) Unready();
                else Ready();
            }
        }

        private void Connected()
        {
            IsConnected = true;

            _indicRenderer.DOKill();
            _indicTransform.DOKill();
            
            indicatorObject.SetActive(true);
            _indicTransform
                .DOScale(1f,0.5f)
                .SetEase(Ease.InOutQuad);
        }

        private void Ready()
        {
            IsReady = true;

            _indicRenderer.DOKill();
            _indicTransform.DOKill();

            _indicRenderer
                .DOColor(validatedColor, 0.1f)
                .SetEase(Ease.InOutQuad);
            
            DOTween
                .Sequence()
                .Append(_indicTransform.DOScale(1.1f, 0.2f).SetEase(Ease.InOutQuad))
                .Append(_indicTransform.DOScale(1f, 0.2f).SetEase(Ease.OutElastic))
                .Play();
        }

        private void Unready()
        {
            IsReady = false;
            
            _indicRenderer.DOKill();
            _indicTransform.DOKill();
            
            _indicRenderer
                .DOColor(_baseColor, 0.1f)
                .SetEase(Ease.InOutQuad);
            
            DOTween
                .Sequence()
                .Append(_indicTransform.DOScale(0.9f, 0.2f).SetEase(Ease.InOutQuad))
                .Append(_indicTransform.DOScale(1f, 0.2f).SetEase(Ease.OutElastic))
                .Play();
        }

        private void Disconnected()
        {
            IsConnected = false;
            IsReady = false;
            
            _indicRenderer.DOKill();
            _indicTransform.DOKill();

            indicatorObject.transform
                .DOScale(0f, 0.5f)
                .OnComplete(() =>
                {
                    indicatorObject.SetActive(false);
                    _indicRenderer.color = _baseColor;
                });
        }
    }
}