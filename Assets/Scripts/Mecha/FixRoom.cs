using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Global;

public class FixRoom : Room
{
    protected void Start()
    {
        
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
