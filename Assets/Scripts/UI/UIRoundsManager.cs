using Global;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIRoundsManager : MonoBehaviour
    {
        [SerializeField]
        private Round round;

        [SerializeField]
        private GameObject lifeBarPrefab;
        [SerializeField]
        private GameObject leftLifeBarWrapper;
        [SerializeField]
        private GameObject rightLifeBarWrapper;

        [SerializeField]
        private GameObject roundPinPrefab;
        [SerializeField]
        private GameObject leftRoundPinWrapper;
        [SerializeField]
        private GameObject rightRoundPinWrapper;

        private void Start()
        {
            round.OnLeftHPChange += UpdateLeftBar;
            round.OnRightHPChange += UpdateRightBar;

            SetupRounds();
        }

        private void SetupRounds()
        {
            for(int i = 0; i < Match.Instance.RequiredWinningRounds; i++)
            {
                Instantiate(roundPinPrefab, leftRoundPinWrapper.transform);
                Instantiate(roundPinPrefab, rightRoundPinWrapper.transform);
            }

            int leftIndex = 0;
            int rightIndex = 0;
            foreach(Round wonRound in Match.Instance.FinishedRounds)
            {
                if (wonRound.WinnerTeam == PlayerManager.Team.Left)
                {
                    leftRoundPinWrapper.transform.GetChild(leftIndex++).GetComponent<UIRoundPin>().DisplayFiller(true);
                } else
                {
                    rightRoundPinWrapper.transform.GetChild(rightIndex++).GetComponent<UIRoundPin>().DisplayFiller(true);
                }
            }

            StartCoroutine(DisableWidthChildControl());
        }

        private IEnumerator DisableWidthChildControl()
        {
            yield return new WaitForSeconds(0.5f);

            leftLifeBarWrapper.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            rightLifeBarWrapper.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            leftLifeBarWrapper.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            rightLifeBarWrapper.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
        }

        public void UpdateLeftBar(int hp)
        {
            UpdateLifeBar(leftLifeBarWrapper, hp);
        }

        public void UpdateRightBar(int hp)
        {

            UpdateLifeBar(rightLifeBarWrapper, hp);
        }

        private void UpdateLifeBar(GameObject lifebar, int hp)
        {
            if (lifebar.transform.childCount > hp)
            {
                List<GameObject> toDestroy = new List<GameObject>();
                for (int i = 0; i < lifebar.transform.childCount - hp; i++)
                {
                    //lifebar.transform.GetChild(i).gameObject.SetActive(false);
                    //toDestroy.Add()
                    Destroy(lifebar.transform.GetChild(i).gameObject);
                }
            } else if (lifebar.transform.childCount < hp)
            {
                for (int i = 0; i <= hp - lifebar.transform.childCount + 1; i++)
                {
                    Instantiate(lifeBarPrefab, lifebar.transform);
                }
            }

            
        }
    }
}
