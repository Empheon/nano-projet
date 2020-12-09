using Global;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class UIRoundsManager : MonoBehaviour
    {
        [SerializeField]
        private Round round;

        [SerializeField]
        private GameObject firstLifeBarPrefab;
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

        [SerializeField]
        private List<Color> color;

        private void Awake()
        {
            round.OnLeftHPChange += UpdateLeftBar;
            round.OnRightHPChange += UpdateRightBar;
        }

        private void Start()
        {
            SetupRounds();
        }

        private void SetupRounds()
        {
            for (int i = 0; i < Match.Instance.RequiredWinningRounds; i++)
            {
                Instantiate(roundPinPrefab, leftRoundPinWrapper.transform);
                Instantiate(roundPinPrefab, rightRoundPinWrapper.transform);
            }

            int leftIndex = Match.Instance.RequiredWinningRounds - 1;
            int rightIndex = 0;
            foreach (Round wonRound in Match.Instance.FinishedRounds)
            {
                if (wonRound.WinnerTeam == PlayerManager.Team.Left)
                {
                    leftRoundPinWrapper.transform.GetChild(leftIndex--).GetComponent<UIRoundPin>().DisplayFiller();
                } else
                {
                    rightRoundPinWrapper.transform.GetChild(rightIndex++).GetComponent<UIRoundPin>().DisplayFiller();
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
            UpdateLifeBar(leftLifeBarWrapper, hp, true);
        }

        public void UpdateRightBar(int hp)
        {

            UpdateLifeBar(rightLifeBarWrapper, hp, false);
        }

        private void UpdateLifeBar(GameObject lifebar, int hp, bool ascending)
        {
            int childCount = lifebar.transform.childCount;
            if (childCount > hp)
            {
                if (ascending)
                {
                    for (int i = childCount - 1; i >= hp; i--)
                    {
                        DestroyLifebar(lifebar.transform.GetChild(i));
                    }
                } else
                {
                    for (int i = 0; i < childCount - hp; i++)
                    {
                        DestroyLifebar(lifebar.transform.GetChild(i));
                    }
                }
            } else if (childCount < hp)
            {
                for (int i = 0; i < hp - childCount; i++)
                {
                    GameObject go;
                    if (i == (ascending ? 0 : hp - childCount - 1))
                    {
                        go = Instantiate(firstLifeBarPrefab, lifebar.transform);
                    } else
                    {
                        go = Instantiate(lifeBarPrefab, lifebar.transform);
                    }
                    if (!ascending)
                    {
                        go.transform.Rotate(Vector3.up * 180);
                    }
                }
            }

            foreach(Image img in lifebar.GetComponentsInChildren<Image>())
            {
                img.color = color[Mathf.Max(hp - 1, 0)];
            }

        }

        private void DestroyLifebar(Transform trsf)
        {
            trsf.DOScale(1.5f, 1.5f).SetEase(Ease.OutCubic);
            trsf.DOLocalMoveY(50, 1.5f).SetEase(Ease.OutCubic);
            trsf.DOLocalMoveZ(-1, 0);

            Image img = trsf.GetComponent<Image>();
            img.DOColor(img.color * new Color(1, 1, 1, 0), 1.55f).OnComplete(() => Destroy(trsf.gameObject));
        }
    }
}
