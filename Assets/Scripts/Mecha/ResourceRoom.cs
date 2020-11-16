using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Global;

public enum ResourceType
{
    AMMUNITION, ENERGY
}

public class ResourceRoom : Room
{
    public ResourceType ResourceType;

    protected void Start()
    {
        
    }

    protected override void DoActionAux(Room room, Action callback)
    {
        // TODO: Do animation
        
        // TODO: Pop resource
    }

    public override bool CanDoAction()
    {
        // TODO: check if 3 items are present in the scene
        return base.CanDoAction();
    }
}
