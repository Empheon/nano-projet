using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace NeoMecha
{
    public class Room : MonoBehaviour
    {
        public bool IsDefended { get; private set; }
        public bool IsJammed { get; private set; }
        public bool IsDamaged { get; private set; }


        public UnityEvent OnDefended;
        public UnityEvent OnDamaged;
        public UnityEvent OnBreakDefence;
        public UnityEvent OnJammed;
        public UnityEvent OnUnJammed;
        public UnityEvent OnFixed;

        public void OnAttackReceived()
        {
            if (IsDefended)
            {
                OnBreakDefenceReceived();
                return;
            }
            if (IsDamaged) return;
            IsDamaged = true;
            OnDamaged.Invoke();
        }

        public void OnDefenceReceived()
        {
            if (IsDefended) return;
            IsDefended = true;
            OnDefended.Invoke();
        }

        public void OnJammingReceived()
        {
            if (IsJammed || IsDamaged) return;
            IsJammed = true;
            OnJammed.Invoke();
        }

        public void OnBreakDefenceReceived()
        {
            if (!IsDefended) return;
            IsDefended = false;
            OnBreakDefence.Invoke();
        }

        public void OnFixReceived()
        {
            if (!IsDamaged) return;
            IsDamaged = false;
            OnFixed.Invoke();
            OnUnjammingReceived();
        }

        public void OnUnjammingReceived()
        {
            if (!IsJammed) return;
            IsJammed = false;
            OnUnJammed.Invoke();
        }


    }
}
