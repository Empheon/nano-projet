using Animations;
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

        [SerializeField]
        private JamBarAnimation jamBarAnimation;

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
            jamBarAnimation.OnUnload();

            yield return new WaitForSeconds(duration);

            room.OnUnjammingReceived();

            m_isJamming = false;
            m_isCoolingDown = true;
            jamBarAnimation.OnLoad(cooldown);

            yield return new WaitForSeconds(cooldown);

            m_isCoolingDown = false;
        }

        public void CancelJamming()
        {
            StopCoroutine(m_coroutine);
            m_currentRoom.OnUnjammingReceived();
            m_isCoolingDown = false;
            m_isJamming = false;
            jamBarAnimation.OnUnload();
        }

        public override bool CanDoAction()
        {
            return base.CanDoAction() && !m_isCoolingDown && !m_isJamming && room.IsFunctional();
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return !room.IsDamaged && !room.IsJammed;
        }
    }
}
