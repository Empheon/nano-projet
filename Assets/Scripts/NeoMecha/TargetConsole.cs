using Character;
using System;
using System.Collections;
using System.Collections.Generic;
using NeoMecha.ConsoleControls;
using UnityEngine;
using UnityEngine.Events;

namespace NeoMecha
{
    
    [Serializable]
    public class RoomEvent : UnityEvent<Room, Action> { }
    
    public abstract class TargetConsole : Console
    {
        [Serializable]
        public struct Target
        {
            public ConsoleTarget Button;
            public Room Room;
        }
        
        public RoomEvent OnActionStart;
        //public RoomEvent OnActionEnd;

        [SerializeField]
        private float actionDuration;

        [SerializeField]
        protected List<Target> TargetList;

        [SerializeField]
        private float refreshFrequency = 10;

        [SerializeField]
        protected Room room;

        [SerializeField]
        private bool forceActionWithoutEventCallback = true;

        private bool m_lockAction;

        protected virtual void Start()
        {
            foreach (Target target in TargetList)
            {
                target.Button.OnValidate.AddListener(() => {
                    if (CanDoAction())
                    {
                        m_lockAction = true;
                        PreAction();
                        OnActionStart.Invoke(target.Room, () => {
                            m_lockAction = false;
                            DoAction(target.Room);
                        });

                        if (forceActionWithoutEventCallback)
                        {
                            m_lockAction = false;
                            DoAction(target.Room);
                        }
                        //StartCoroutine(StartAction(target.Room));
                    }
                });
            }

            StartCoroutine(RefreshButtonVisibility());
        }

        protected virtual void PreAction() { }

        private IEnumerator RefreshButtonVisibility()
        {
            for (;;)
            {
                foreach (Target target in TargetList)
                {
                    var targetable = IsRoomTargetable(target.Room);
                    if (targetable && !target.Button.IsActive)
                    {
                        target.Button.Activate();
                    }
                    else if (!targetable && target.Button.IsActive)
                    {
                        target.Button.Desactivate();
                    }
                }

                yield return new WaitForSeconds(1 / refreshFrequency);
            }
        }

        protected abstract bool IsRoomTargetable(Room room);

        //private IEnumerator StartAction(Room room)
        //{
        //    OnActionStart.Invoke(room, () => DoAction(room));

        //    if (OnActionStart.GetPersistentEventCount() == 0)
        //    {
        //        DoAction(room);
        //    }

        //    yield return new WaitForSeconds(actionDuration);

        //    //OnActionEnd.Invoke(room);
        //    DoAction(room);
        //}

        protected abstract void DoAction(Room room);

        public virtual bool CanDoAction()
        {
            return !m_lockAction;
        }

        public override bool CanInteract(GameObject character)
        {
            bool isTargettable = false;
            foreach(Target target in TargetList)
            {
                if (IsRoomTargetable(target.Room))
                {
                    isTargettable = true;
                    break;
                }
            }

            return CanDoAction() && isTargettable;
        }
    }
}
