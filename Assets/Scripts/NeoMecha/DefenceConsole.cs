using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NeoMecha
{
    public class DefenceConsole : TargetConsole
    {
        [SerializeField]
        private LoadConsole m_loadConsole;

        protected override void DoAction(Room room)
        {
            foreach (Target targetInList in TargetList)
            {
                if (targetInList.Room == room)
                {
                    targetInList.Room.OnDefenceReceived();
                } else
                {
                    targetInList.Room.OnBreakDefenceReceived();
                }
            }
            m_loadConsole.IsLoaded = false;
        }

        public override bool CanDoAction()
        {
            return base.CanDoAction() && m_loadConsole.IsLoaded && room.IsFunctional();
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return !room.IsDefended;
        }
    }
}
