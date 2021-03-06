﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animations
{
    public class ConnexionCircle : MonoBehaviour
    {
        [SerializeField] private Color validatedColor;
        [SerializeField] private Image renderer;

        private Color _baseColor;
        private Transform _transform;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            //renderer = GetComponent<Image>();
            _baseColor = renderer.color;
            
            gameObject.SetActive(false);
            _transform.localScale = Vector3.zero;
        }

        public void Appear()
        {
            _transform.DOKill();
            
            gameObject.SetActive(true);
            _transform
                .DOScale(1f,0.5f)
                .SetEase(Ease.InOutQuad);
        }

        public void Validate()
        {
            renderer.DOKill();
            _transform.DOKill();

            renderer
                .DOColor(validatedColor, 0.1f)
                .SetEase(Ease.InOutQuad);
            
            DOTween
                .Sequence()
                .Append(_transform.DOScale(1.1f, 0.2f).SetEase(Ease.InOutQuad))
                .Append(_transform.DOScale(1f, 0.2f).SetEase(Ease.OutElastic))
                .Play();
        }

        public void UnValidate()
        {
            renderer.DOKill();
            _transform.DOKill();
            
            renderer
                .DOColor(_baseColor, 0.1f)
                .SetEase(Ease.InOutQuad);
            
            DOTween
                .Sequence()
                .Append(_transform.DOScale(0.9f, 0.2f).SetEase(Ease.InOutQuad))
                .Append(_transform.DOScale(1f, 0.2f).SetEase(Ease.OutElastic))
                .Play();
        }

        public void Disappear()
        {
            _transform.DOKill();

            _transform
                .DOScale(0f, 0.5f)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    renderer.color = _baseColor;
                });
        }
    }
}