using System.Collections;
using DG.Tweening;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu
{
    public enum GamepadButton
    {
        NORTH, SOUTH, EAST, WEST
    }

    public class GamepadCheck : MonoBehaviour
    {
        [SerializeField] protected GameObject indicatorObject;
        [SerializeField] private PlayerManager.Team team;

        [SerializeField] private Color validatedColor;

        [SerializeField] private float checkFrequency = 10;

        [SerializeField] private GamepadButton buttonToCheck = GamepadButton.SOUTH;
        
        private Gamepad _gamepad;
        protected Image _indicRenderer;
        protected Transform _indicTransform;
        
        protected Color _baseColor;

        public bool IsConnected { get; protected set; } = false;
        public bool IsReady { get; private set; } = false;

        private void OnDisable()
        {
            if (_indicRenderer == null || _indicTransform == null) return;

            // force "unready" without anim
            IsReady = false;

            _indicRenderer.DOKill();
            _indicTransform.DOKill();

            if (_baseColor != null)
            {
                _indicRenderer.color = _baseColor;
            }
        }

        protected virtual void Setup()
        {
            indicatorObject.SetActive(false);
            _indicTransform = indicatorObject.GetComponent<Transform>();
            //_indicTransform.localScale = Vector3.zero;
            _indicRenderer = indicatorObject.GetComponent<Image>();
            _baseColor = _indicRenderer.color;
        }


        private IEnumerator Start()
        {
            Setup();
            
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
            if (IsConnected && CheckButtonPressed())
            {
                if (IsReady) Unready();
                else Ready();
            }
        }

        private bool CheckButtonPressed()
        {
            switch(buttonToCheck)
            {
                case GamepadButton.NORTH:
                    return _gamepad.buttonNorth.wasPressedThisFrame;
                case GamepadButton.SOUTH:
                    return _gamepad.buttonSouth.wasPressedThisFrame;
                case GamepadButton.EAST:
                    return _gamepad.buttonEast.wasPressedThisFrame;
                case GamepadButton.WEST:
                    return _gamepad.buttonWest.wasPressedThisFrame;
                default:
                    return false;
            }
        }

        protected virtual void Connected()
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

        protected virtual void Disconnected()
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