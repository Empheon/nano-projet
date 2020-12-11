using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Animations;

namespace NeoMecha
{
    public class AttackConsole : TargetConsole
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
            m_loadConsole.UnLoad();
        }

        protected override void DoAction(Room room)
        {
            room.OnAttackReceived();
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
