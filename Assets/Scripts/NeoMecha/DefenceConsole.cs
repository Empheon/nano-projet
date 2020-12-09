using Animations;
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
        [SerializeField]
        private ConsoleSpriteSwitcher consoleSpriteSwitcher;

        protected override void Start()
        {
            base.Start();
            room.OnDamaged.AddListener(consoleSpriteSwitcher.OnBreak);
            room.OnFixed.AddListener(consoleSpriteSwitcher.OnFix);
        }
        protected override void PreAction()
        {
            base.PreAction();
            consoleSpriteSwitcher.OnActivate();
        }

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
            m_loadConsole.UnLoad();
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
