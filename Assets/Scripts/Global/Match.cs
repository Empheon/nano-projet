using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Global
{
    public class Match : MonoBehaviour
    {
        [SerializeField] private int gameSceneIndex = -1;
        [SerializeField] private int menuSceneIndex = -1;

        public static Match Instance { get; private set; }

        public int RequiredWinningRounds;

        public List<Round> FinishedRounds { get; private set; }

        private void Start()
        {
            Instance = this;
            Reset();
        }

        private void Reset()
        {
            FinishedRounds = new List<Round>();
        }

        public void FinishRound(Round round)
        {
            FinishedRounds.Add(round);

            int leftWonRounds = WonRounds(PlayerManager.Team.Left);
            int rightWonRounds = WonRounds(PlayerManager.Team.Right);

            if (leftWonRounds < RequiredWinningRounds && rightWonRounds < RequiredWinningRounds)
            {
                DOTween.KillAll();
                SceneManager.LoadScene(gameSceneIndex);
            } else
            {
                DOTween.KillAll();
                Reset();
                SceneManager.LoadScene(menuSceneIndex);
            }
        }

        private int WonRounds(PlayerManager.Team team)
        {
            return FinishedRounds.Where((x) => x.WinnerTeam == team).ToArray().Length;
        }
    }
}
