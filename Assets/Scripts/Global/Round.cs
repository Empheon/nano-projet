﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using UI;
using UnityEngine;
using UnityEngine.Events;
using static Global.PlayerManager;
using CharacterController = Character.CharacterController;

namespace Global
{
    public class Round : MonoBehaviour
    {
        [SerializeField] private int baseHitPoints;
        [SerializeField] private Timer timer;
        
        [Header("Round Start")] 
        [SerializeField] private float beforeRoundDuration = 0.5f;
        [SerializeField] private float roundNumberAnnouncementDuration = 2.5f;
        [SerializeField] private CharacterSpawner spawner;
        
        [Header("Round End")]
        [SerializeField] private float secondsBeforeNextRound;
        [SerializeField] private ParticleSystem mechaExplosionLeft;
        [SerializeField] private ParticleSystem mechaExplosionRight;
        
        private Dictionary<Team, int> m_teamHp;

        public Action<int> OnLeftHPChange;
        public Action<int> OnRightHPChange;

        public UnityEvent OnRound1Start;
        public UnityEvent OnRound2Start;
        public UnityEvent OnRound3Start;

        public Team WinnerTeam { get; private set; }

        private IEnumerator Start()
        {
            // setup HPs
            m_teamHp = new Dictionary<Team, int>(2);

            m_teamHp[Team.Left] = baseHitPoints;
            m_teamHp[Team.Right] = baseHitPoints;

            OnLeftHPChange?.Invoke(m_teamHp[Team.Left]);
            OnRightHPChange?.Invoke(m_teamHp[Team.Right]);
            
            UpdateSound();
            AkSoundEngine.PostEvent("Music_Combat_Start", gameObject);
            
            // wait for characters to spawn
            yield return new WaitForEndOfFrame();
            
            // prevent characters from moving
            foreach (var character in spawner.Characters)
            {
                var controller = character.GetComponent<CharacterController>();
                controller.enabled = false;
            }
            
            yield return new WaitForSeconds(beforeRoundDuration);
            
            // play right sound announcing round number
            string voiceEvent; 
            switch (Match.Instance.FinishedRounds.Count)
            {
                case 0: 
                    voiceEvent = "Voice_Gameplay_Round1";
                    OnRound1Start.Invoke();
                    break;
                case 1: 
                    voiceEvent = "Voice_Gameplay_Round2";
                    OnRound2Start.Invoke(); 
                    break;
                case 2: default:
                    voiceEvent = "Voice_Gameplay_FinalRound";
                    OnRound3Start.Invoke(); 
                    break;
            }
            
            AkSoundEngine.PostEvent(voiceEvent, gameObject);
            yield return new WaitForSeconds(roundNumberAnnouncementDuration);

            // say "Fight !" and give controls to player
            foreach (var character in spawner.Characters)
            {
                var controller = character.GetComponent<CharacterController>();
                controller.enabled = true;
            }
            
            AkSoundEngine.PostEvent("Voice_Gameplay_Fight", gameObject);
        }

        private void OnDestroy()
        {
            AkSoundEngine.PostEvent("Music_Combat_Stop", gameObject);
        }

        public void LeftGetHit()
        {
            m_teamHp[Team.Left] -= 1;
            OnLeftHPChange?.Invoke(m_teamHp[Team.Left]);
            CheckEndGame(Team.Left);
            UpdateSound();
        }

        public void RightGetHit()
        {
            m_teamHp[Team.Right] -= 1;
            OnRightHPChange?.Invoke(m_teamHp[Team.Right]);
            CheckEndGame(Team.Right);
            UpdateSound();
        }

        private void CheckEndGame(Team team)
        {
            if (m_teamHp[team] <= 0)
            {
                WinnerTeam = team == Team.Left ? Team.Right : Team.Left;

                StartCoroutine(ExplodeAndFinishRound());
            }
        }

        private void UpdateSound()
        {
            if (m_teamHp.Any(pair => pair.Value <= 1))
            {
                AkSoundEngine.SetSwitch("Music_Switch", "Combat_TimeOut", gameObject);
            }
            else if(m_teamHp.Any(pair => pair.Value <= 2))
            {
                AkSoundEngine.SetSwitch("Music_Switch", "Combat_LowHealth", gameObject);
            }
            else
            {
                AkSoundEngine.SetSwitch("Music_Switch", "Combat_Begin", gameObject);
            }
        }

        private IEnumerator ExplodeAndFinishRound()
        {
            // prevent characters from moving
            foreach (var character in spawner.Characters)
            {
                var controller = character.GetComponent<CharacterController>();
                controller.Stop();
                controller.enabled = false;
            }
            
            var expl = WinnerTeam == Team.Left ? mechaExplosionRight : mechaExplosionLeft;
            expl.Play();
            
            AkSoundEngine.PostEvent("Tower_Defeat_Destroyed", gameObject);
            
            yield return new WaitForSeconds(secondsBeforeNextRound);
            
            Match.Instance.FinishRound(this);
        }

        public void Timeout()
        {
            if (m_teamHp[Team.Left] == m_teamHp[Team.Right])
            {
                WinnerTeam = Team.None;
            } else
            {
                WinnerTeam = m_teamHp[Team.Left] > m_teamHp[Team.Right] ? Team.Left : Team.Right;
            }

            //Trigger Anim

            Match.Instance.FinishRound(this);
        }
    }
}
