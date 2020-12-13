using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animations;
using UnityEngine;
using DG.Tweening;
using Global.Loading;

namespace Global
{
    public class Match : MonoBehaviour
    {
        [SerializeField] private int gameSceneIndex = -1;
        [SerializeField] private int menuSceneIndex = -1;

        [SerializeField] private WinScreen winScreenPrefab;
        [SerializeField] private float winScreenDisplayTime;

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
            if (round.WinnerTeam == PlayerManager.Team.None)
            {
                DOTween.KillAll();
                LoadingScreen.Instance.LoadScene(gameSceneIndex);
                return;
            }

            FinishedRounds.Add(round);

            int leftWonRounds = WonRounds(PlayerManager.Team.Left);
            int rightWonRounds = WonRounds(PlayerManager.Team.Right);

            if (leftWonRounds < RequiredWinningRounds && rightWonRounds < RequiredWinningRounds)
            {
                DOTween.KillAll();
                LoadingScreen.Instance.LoadScene(gameSceneIndex);
            } else
            {
                DOTween.KillAll();
                Reset();
                
                StartCoroutine(WinScreenAndMenu(
                    leftWonRounds == RequiredWinningRounds ? 
                        PlayerManager.Team.Left : 
                        PlayerManager.Team.Right));
            }
        }

        private IEnumerator WinScreenAndMenu(PlayerManager.Team winningTeam)
        {
            WinScreen screen = Instantiate(winScreenPrefab, Vector3.zero, Quaternion.identity);
            screen.DisplayWinner(winningTeam);
            
            AkSoundEngine.SetSwitch("Music_Switch", "Combat_End", GameObject.Find("Round"));
            
            yield return new WaitForSeconds(winScreenDisplayTime);
            
            LoadingScreen.Instance.LoadScene(menuSceneIndex);
        }
        
        private int WonRounds(PlayerManager.Team team)
        {
            return FinishedRounds.Where((x) => x.WinnerTeam == team).ToArray().Length;
        }
    }
}
