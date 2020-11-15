using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Global;

public class DefenceRoom : Room
{
    protected void Start()
    {
        m_actionCooldown = GameSettings.Instance.DefenceCooldown;
    }
    protected override void OnAttackReceived()
    {
        base.OnAttackReceived();
        m_lightAtt.color = Color.red;
    }

    protected override void OnDefenceReceived()
    {
        base.OnDefenceReceived();
        m_lightDef.color = Color.cyan;
    }

    protected override void OnJammingReceived()
    {
        base.OnJammingReceived();
        m_lightJam.color = Color.yellow;
    }

    protected override void OnBreakDefenceReceived()
    {
        base.OnBreakDefenceReceived();
        m_lightDef.color = Color.white;
    }

    protected override void OnFixReceived()
    {
        base.OnFixReceived();
        m_lightAtt.color = Color.white;
    }

    protected override void OnUnjammingReceived()
    {
        base.OnUnjammingReceived();
        m_lightJam.color = Color.white;
    }

    protected override void DoActionAux(Room room, Action callback)
    {
        // TODO: Do animation
        Sequence seq = DOTween.Sequence();
        seq.Append(gameObject.transform.DOScale(1.5f, 0.5f));
        seq.Append(gameObject.transform.DOScale(1f, 0.5f));
        seq.Append(room.transform.DOScale(1.5f, 0.5f));
        seq.Append(room.transform.DOScale(1f, 0.5f));

        seq.OnComplete(() => callback());
    }
}
