using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NeoMecha
{
    
    [Serializable]
    public class RoomEvent : UnityEvent<Room> { }
    
    public abstract class TargetConsole : MonoBehaviour
    {
        [Serializable]
        public struct Target
        {
            public ConsolePanelButton Button;
            public Room Room;
        }
        
        public RoomEvent OnActionStart;
        public RoomEvent OnActionEnd;

        [SerializeField]
        private float actionDuration;

        [SerializeField]
        protected List<Target> TargetList;

        [SerializeField]
        private float refreshFrequency = 10;

        [SerializeField]
        protected Room room;

        private void Start()
        {
            foreach (Target target in TargetList)
            {
                target.Button.OnValidate.AddListener(() => {
                    if (CanDoAction())
                    {
                        StartCoroutine(StartAction(target.Room));
                    }
                });
            }

            StartCoroutine(RefreshButtonVisibility());
        }

        private IEnumerator RefreshButtonVisibility()
        {
            for (; ; )
            {
                foreach (Target target in TargetList)
                {
                    target.Button.IsActive = IsRoomTargetable(target.Room);
                }

                yield return new WaitForSeconds(1 / refreshFrequency);
            }
        }

        protected abstract bool IsRoomTargetable(Room room);

        private IEnumerator StartAction(Room room)
        {
            OnActionStart.Invoke(room);

            yield return new WaitForSeconds(actionDuration);

            OnActionEnd.Invoke(room);
            DoAction(room);
        }

        protected abstract void DoAction(Room room);

        public virtual bool CanDoAction()
        {
            return true;
        }
    }
}
