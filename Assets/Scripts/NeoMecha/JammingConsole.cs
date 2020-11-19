using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NeoMecha
{
    public class JammingConsole : TargetConsole
    {
        [SerializeField]
        private float duration;
        [SerializeField]
        private float cooldown;

        private bool m_isCoolingDown;
        private bool m_isJamming;

        private Coroutine m_coroutine;

        private Room m_currentRoom;

        protected override void DoAction(Room room)
        {
            m_currentRoom = room;
            m_coroutine = StartCoroutine(DoJam(room));
        }

        private IEnumerator DoJam(Room room)
        {
            room.OnJammingReceived();
            m_isJamming = true;

            yield return new WaitForSeconds(duration);

            room.OnUnjammingReceived();

            m_isJamming = false;
            m_isCoolingDown = true;

            yield return new WaitForSeconds(duration);

            m_isCoolingDown = false;
        }

        public void CancelJamming()
        {
            StopCoroutine(m_coroutine);
            m_currentRoom.OnUnjammingReceived();
            m_isCoolingDown = false;
            m_isJamming = false;
        }

        public override bool CanDoAction()
        {
            return !m_isCoolingDown && !m_isJamming;
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return !room.IsDamaged && !room.IsJammed;
        }
    }
}
