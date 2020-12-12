using System;
using Global;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform teamLeftContainer;
        [SerializeField] private RectTransform teamRightContainer;
        
        [SerializeField] private RectTransform winnerPrefab;
        [SerializeField] private RectTransform loserPrefab;
        
        private Animator _animator;
        private PlayerManager.Team _winner;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void DisplayWinner(PlayerManager.Team winner)
        {
            _winner = winner;
            Instantiate(winner == PlayerManager.Team.Left ? winnerPrefab : loserPrefab, teamLeftContainer);
            Instantiate(winner == PlayerManager.Team.Right ? winnerPrefab : loserPrefab, teamRightContainer);
            
            _animator.SetTrigger("Close");
        }

        public void SayWinner()
        {
            var winSoundEvent = _winner == PlayerManager.Team.Left
                ? "Voice_Gameplay_Player1Wins"
                : "Voice_Gameplay_Player2Wins";
            AkSoundEngine.PostEvent(winSoundEvent, gameObject);
        }
    }
}