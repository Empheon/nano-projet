using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Global
{
    public class Match : MonoBehaviour
    {
        public static Match Instance { get; private set; }

        public int RequiredWinningRounds;

        public List<Round> FinishedRounds { get; private set; }

        private void Start()
        {
            Instance = this;
            FinishedRounds = new List<Round>();
        }

        public void FinishRound(Round round)
        {
            FinishedRounds.Add(round);

            int leftWonRounds = WonRounds(PlayerManager.Team.Left);
            int rightWonRounds = WonRounds(PlayerManager.Team.Right);

            if (leftWonRounds < RequiredWinningRounds && rightWonRounds < RequiredWinningRounds)
            {

            }
        }

        private int WonRounds(PlayerManager.Team team)
        {
            return FinishedRounds.Where((x) => x.WinnerTeam == team).ToArray().Length;
        }
    }
}
