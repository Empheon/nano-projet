using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Character;
using UnityEngine;
using static Global.PlayerManager;
using CharacterController = Character.CharacterController;

namespace Global
{
    public class Round : MonoBehaviour
    {
        [SerializeField] private int baseHitPoints;

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
        public Team WinnerTeam { get; private set; }

        private IEnumerator Start()
        {
            // setup HPs
            m_teamHp = new Dictionary<Team, int>(2);

            m_teamHp[Team.Left] = baseHitPoints;
            m_teamHp[Team.Right] = baseHitPoints;

            OnLeftHPChange?.Invoke(m_teamHp[Team.Left]);
            OnRightHPChange?.Invoke(m_teamHp[Team.Right]);
            
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
                case 0: voiceEvent = "Voice_Gameplay_Round1"; break;
                case 1: voiceEvent = "Voice_Gameplay_Round2"; break;
                case 2: default: voiceEvent = "Voice_Gameplay_FinalRound"; break;
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

        public void LeftGetHit()
        {
            m_teamHp[Team.Left] -= 1;
            OnLeftHPChange?.Invoke(m_teamHp[Team.Left]);
            CheckEndGame(Team.Left);
        }

        public void RightGetHit()
        {
            m_teamHp[Team.Right] -= 1;
            OnRightHPChange?.Invoke(m_teamHp[Team.Right]);
            CheckEndGame(Team.Right);
        }

        private void CheckEndGame(Team team)
        {
            if (m_teamHp[team] <= 0)
            {
                WinnerTeam = team == Team.Left ? Team.Right : Team.Left;

                StartCoroutine(ExplodeAndFinishRound());
            }
        }

        private IEnumerator ExplodeAndFinishRound()
        {
            var expl = WinnerTeam == Team.Left ? mechaExplosionRight : mechaExplosionLeft;
            expl.Play();
            
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
