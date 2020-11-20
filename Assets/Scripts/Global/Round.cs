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

        public Team WinnerTeam;

        private void Start()
        {
            m_teamHp = new Dictionary<Team, int>(2);

            m_teamHp[Team.Left] = baseHitPoints;
            m_teamHp[Team.Right] = baseHitPoints;

            OnLeftHPChange?.Invoke(m_teamHp[Team.Left]);
            OnLeftHPChange?.Invoke(m_teamHp[Team.Right]);
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
            OnLeftHPChange?.Invoke(m_teamHp[Team.Right]);
            CheckEndGame(Team.Right);
        }

        private void CheckEndGame(Team team)
        {
            if (m_teamHp[team] <= 0)
            {
                WinnerTeam = team;
                Match.Instance.FinishRound(this);
            }
        }
    }
}
