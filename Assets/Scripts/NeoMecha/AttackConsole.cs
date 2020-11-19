﻿using System;
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
            m_loadConsole.IsLoaded = false;
        }

        protected override bool CanDoAction()
        {
            return m_loadConsole.IsLoaded;
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return true;
        }
    }
}