﻿using System;
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
    private bool m_prevActionStatus = true;

    public bool IsProtected { get; private set; }
    public bool IsJammed { get; private set; }
    public bool IsDamaged { get; private set; }

    protected float m_actionCooldown;
    protected float m_timeAtLastAction;


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
                OnDefenceReceived();
                break;
            case MechaActionType.JAMMING:
                OnJammingReceived();
                break;
            case MechaActionType.FIX:
                OnFixReceived();
                break;
            case MechaActionType.UNJAM:
                OnUnjammingReceived();
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

    public void DoAction(Room room, Action callback)
    {
        if (CanDoAction())
        {
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
