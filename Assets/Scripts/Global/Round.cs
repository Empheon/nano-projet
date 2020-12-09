using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Global.PlayerManager;

namespace Global
{
    public class Round : MonoBehaviour
    {
        [SerializeField]
        private int baseHitPoints;

        private Dictionary<Team, int> m_teamHp;

        public Action<int> OnLeftHPChange;
        public Action<int> OnRightHPChange;

        [HideInInspector]
        public Team WinnerTeam { get; private set; }

        private void Start()
        {
            m_teamHp = new Dictionary<Team, int>(2);

            m_teamHp[Team.Left] = baseHitPoints;
            m_teamHp[Team.Right] = baseHitPoints;

            OnLeftHPChange?.Invoke(m_teamHp[Team.Left]);
            OnRightHPChange?.Invoke(m_teamHp[Team.Right]);
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

                //Trigger Anim

                Match.Instance.FinishRound(this);
            }
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
