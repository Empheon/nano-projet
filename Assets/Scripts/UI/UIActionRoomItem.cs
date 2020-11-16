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

    private MechaActionType m_actionType;

    public Action<RoomType, MechaActionType> OnDoAction;

    public void Init(MechaActionType actionType)
    {
        m_actionType = actionType;
    }

    public void DoAction()
    {
        OnDoAction?.Invoke(m_targetRoomType, m_actionType);
    }
}

