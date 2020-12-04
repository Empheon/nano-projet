using NeoMecha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Animations
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float deployDuration;

        private Animator _animator;
        private ParticleSystem _shieldParticles;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Deploy()
        {
            _animator.SetBool("Opened", true);
            StartCoroutine(WaitAndSetParticles(true, deployDuration));
        }

        public void Retract()
        {
            _animator.SetBool("Opened", false);
            StartCoroutine(WaitAndSetParticles(false, deployDuration));
        }

        private IEnumerator WaitAndSetParticles(bool activeOrNot, float waitFor)
        {
            yield return new WaitForSeconds(waitFor);
            
            if (activeOrNot) _shieldParticles.Play(); 
            else _shieldParticles.Stop(); 
        }
    }
}
