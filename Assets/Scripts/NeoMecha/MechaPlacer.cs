using Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeoMecha
{
    public class MechaPlacer : MonoBehaviour
    {
        [SerializeField]
        private GameObject leftMecha;
        [SerializeField]
        private GameObject rightMecha;
        [SerializeField]
        private GameObject leftSpawn;
        [SerializeField]
        private GameObject rightSpawn;

        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private float offset;

        private void Awake()
        {
            Vector3 stageDimensions = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            float newX = stageDimensions.x - offset;

            Vector3 pos = leftMecha.transform.position;
            pos.x = -newX;
            leftMecha.transform.position = pos;

            pos = rightMecha.transform.position;
            pos.x = newX;
            rightMecha.transform.position = pos;

            pos = leftSpawn.transform.position;
            pos.x = -newX;
            leftSpawn.transform.position = pos;

            pos = rightSpawn.transform.position;
            pos.x = newX;
            rightSpawn.transform.position = pos;


            float pathOffset = 6 - newX;
            Canon c = leftMecha.GetComponentInChildren<Canon>();
            c.UpdatePathsWithOffset(pathOffset * (3 / leftMecha.transform.localScale.x));
            c = rightMecha.GetComponentInChildren<Canon>();
            c.UpdatePathsWithOffset(pathOffset * (3 / rightMecha.transform.localScale.x));
        }
    }
}
