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
    public class FixConsole : TargetConsole
    {
        protected override void DoAction(Room room)
        {
            room.OnFixReceived();
        }

        protected override bool IsRoomTargetable(Room room)
        {
            return room.IsDamaged;
        }
    }
}
