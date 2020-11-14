using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha : MonoBehaviour
{
    [SerializeField]
    private AttackRoom m_attackRoom;
    [SerializeField]
    private DefenceRoom m_defenceRoom;
    [SerializeField]
    private JammingRoom m_jammingRoom;

    public Mecha m_otherMecha;

    // Temporary to delete
    public int roomIndex;
    public MechaActionType type;
    public bool activate;

    public void Init(Mecha otherMecha)
    {
        m_otherMecha = otherMecha;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activate)
        {
            return;
        }
        activate = false;

        switch (roomIndex)
        {
            case 1:
                m_attackRoom.OnReceive(type);
                break;
            case 2:
                m_defenceRoom.OnReceive(type);
                break;
            case 3:
                m_jammingRoom.OnReceive(type);
                break;
        }
    }

    //public void DisplayActionPanel(RoomType roomType)
    //{
    //    switch (roomType)
    //    {
    //        case RoomType.ATTACK:
    //            m_attackRoom.DisplayPanel();
    //            break;
    //        case RoomType.DEFENCE:
    //            m_defenceRoom.DisplayPanel();
    //            break;
    //        case RoomType.JAMMING:
    //            m_jammingRoom.DisplayPanel();
    //            break;
    //    }
    //}

    //public void DoAttackAction(RoomType roomType)
    //{
    //    switch (roomType)
    //    {
    //        case RoomType.ATTACK:
    //            m_attackRoom.DoAction(m_otherMecha.GetRoom(roomType),
    //                                 () => m_otherMecha.ReceiveAction(MechaActionType.ATTACK, roomType));
    //            break;
    //        case RoomType.DEFENCE:
    //            m_defenceRoom.DoAction(GetRoom(roomType),
    //                                 () => ReceiveAction(MechaActionType.DEFENCE, roomType));
    //            break;
    //        case RoomType.JAMMING:
    //            m_jammingRoom.DoAction(m_otherMecha.GetRoom(roomType),
    //                                 () => m_otherMecha.ReceiveAction(MechaActionType.JAMMING, roomType));
    //            break;
    //    }
    //}

    public void DoAction(MechaActionType mechaActionType, RoomType targetRoomType)
    {
        switch (mechaActionType)
        {
            case MechaActionType.ATTACK:
                m_attackRoom.DoAction(m_otherMecha.GetRoom(targetRoomType),
                                     () => m_otherMecha.ReceiveAction(MechaActionType.ATTACK, targetRoomType));
                break;
            case MechaActionType.DEFENCE:
                m_defenceRoom.DoAction(GetRoom(targetRoomType),
                                     () => ReceiveAction(MechaActionType.DEFENCE, targetRoomType));
                break;
            case MechaActionType.JAMMING:
                m_jammingRoom.DoAction(m_otherMecha.GetRoom(targetRoomType),
                                     () => m_otherMecha.ReceiveAction(MechaActionType.JAMMING, targetRoomType));
                break;
        }
    }

    public void ReceiveAction(MechaActionType actionType, RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.ATTACK:
                m_attackRoom.OnReceive(actionType);
                break;
            case RoomType.DEFENCE:
                m_defenceRoom.OnReceive(actionType);
                break;
            case RoomType.JAMMING:
                m_jammingRoom.OnReceive(actionType);
                break;
        }
    }

    public Room GetRoom(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.ATTACK:
                return m_attackRoom;
            case RoomType.DEFENCE:
                return m_defenceRoom;
            case RoomType.JAMMING:
                return m_jammingRoom;
        }
        return null;
    }
}
