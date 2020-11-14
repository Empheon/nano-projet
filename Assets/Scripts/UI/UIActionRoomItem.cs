using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIActionRoomItem : MonoBehaviour
{
    [SerializeField]
    private RoomType m_targetRoomType;

    private Mecha m_mecha;
    private MechaActionType m_actionType;

    public void Init(Mecha mecha, MechaActionType actionType)
    {
        m_mecha = mecha;
        m_actionType = actionType;
    }

    public void DoAction()
    {
        m_mecha.DoAction(m_actionType, m_targetRoomType);
    }
}

