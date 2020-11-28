using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;

namespace NeoMecha
{
    public class FixConsole : TargetConsole
    {
        private bool m_isFixing;

        protected override void DoAction(Room room)
        {
            StartCoroutine(DelayAction(room));
        }

        private IEnumerator DelayAction(Room room)
        {
            m_isFixing = true;
            yield return new WaitForSeconds(3);
            m_isFixing = false;
            room.OnFixReceived();
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return room.IsDamaged;
        }

        public override bool CanDoAction()
        {
            return base.CanDoAction() && !m_isFixing;
        }
    }
}
