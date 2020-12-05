using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace NeoMecha
{
    public class AttackConsole : TargetConsole
    {
        [SerializeField]
        private LoadConsole m_loadConsole;

        protected override void DoAction(Room room)
        {
            room.OnAttackReceived();
            m_loadConsole.UnLoad();
        }

        public override bool CanDoAction()
        {
            return base.CanDoAction() && m_loadConsole.IsLoaded && room.IsFunctional();
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return true;
        }
    }
}
