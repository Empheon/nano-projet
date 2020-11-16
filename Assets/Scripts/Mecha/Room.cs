using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum RoomType
{
    ATTACK, DEFENCE, JAMMING
}

public abstract class Room : MonoBehaviour
{
    [SerializeField]
    protected Light2D m_lightDef;
    [SerializeField]
    protected Light2D m_lightAtt;
    [SerializeField]
    protected Light2D m_lightJam;

    public Action<bool> OnActionableStatusChanged;
    public Action<bool> OnFixableStatusChanged;
    public Action<bool> OnLoadableStatusChanged;
    private bool m_prevActionStatus = true;

    public bool IsProtected { get; private set; }
    public bool IsJammed { get; private set; }
    public bool IsDamaged { get; private set; }
    public bool IsLoaded { get; private set; }

    protected float m_actionCooldown;
    protected float m_timeAtLastAction = float.NegativeInfinity;


    private void Update()
    {
        // Update UI visibility when the action is possible
        bool tmpActionStatus = CanDoAction();
        if (m_prevActionStatus != tmpActionStatus)
        {
            OnActionableStatusChanged?.Invoke(tmpActionStatus);
            m_prevActionStatus = tmpActionStatus;
        }
    }

    public void OnReceive(MechaActionType type)
    {
        switch (type)
        {
            case MechaActionType.ATTACK:
                if (IsProtected)
                {
                    OnBreakDefenceReceived();
                    return;
                }
                OnAttackReceived();
                break;
            case MechaActionType.DEFENCE:
                if (IsProtected)
                {
                    return;
                }
                OnDefenceReceived();
                break;
            case MechaActionType.JAMMING:
                if (IsJammed)
                {
                    return;
                }
                OnJammingReceived();
                break;
            case MechaActionType.FIX:
                if (!IsDamaged)
                {
                    return;
                }
                OnFixReceived();
                break;
            case MechaActionType.UNJAM:
                if (!IsJammed)
                {
                    return;
                }
                OnUnjammingReceived();
                break;
            case MechaActionType.LOAD:
                if (IsLoaded)
                {
                    return;
                }
                OnLoadReceived();
                break;
            case MechaActionType.BREAK_DEFENCE:
                if (!IsProtected)
                {
                    return;
                }
                OnBreakDefenceReceived();
                break;
        }
    }

    protected virtual void OnAttackReceived()
    {
        IsDamaged = true;
        OnFixableStatusChanged?.Invoke(true);
    }

    protected virtual void OnDefenceReceived()
    {
        IsProtected = true;
    }

    protected virtual void OnJammingReceived()
    {
        IsJammed = true;
    }

    protected virtual void OnBreakDefenceReceived()
    {
        IsProtected = false;
    }

    protected virtual void OnFixReceived()
    {
        IsDamaged = false;
        OnFixableStatusChanged?.Invoke(false);
        OnUnjammingReceived();
    }

    protected virtual void OnUnjammingReceived()
    {
        IsJammed = false;
    }

    protected virtual void OnLoadReceived()
    {
        IsLoaded = true;
        OnLoadableStatusChanged?.Invoke(false);
    }

    protected virtual void OnUnLoadReceived()
    {
        IsLoaded = false;
        OnLoadableStatusChanged?.Invoke(true);
    }

    public void DoAction(Room room, Action callback)
    {
        if (CanDoAction())
        {
            OnUnLoadReceived();
            DoActionAux(room, callback);
            m_timeAtLastAction = Time.time;
        }
    }

    // May be overriden for rooms where loaded resources are needed
    public virtual bool CanDoAction()
    {
        return (Time.time - m_timeAtLastAction > m_actionCooldown) && !IsDamaged && !IsJammed;
    }

    protected abstract void DoActionAux(Room room, Action callback);

    
}
