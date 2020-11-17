using Global;
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
    [SerializeField]
    private FixRoom m_fixRoom;
    [SerializeField]
    private ResourceRoom m_munRoom;
    [SerializeField]
    private ResourceRoom m_eneRoom;

    public Mecha m_otherMecha;

    private Dictionary<RoomType, float> m_roomJammedTimes;

    private void Start()
    {
        m_roomJammedTimes = new Dictionary<RoomType, float>();
    }

    public void Init(Mecha otherMecha)
    {
        m_otherMecha = otherMecha;
    }

    private void Update()
    {
        // Check when the jamming is finished
        float now = Time.time;
        foreach (var kv in m_roomJammedTimes)
        {
            Room room = GetRoom(kv.Key);
            if (room.IsJammed && now - kv.Value > GameSettings.Instance.JammingDuration)
            {
                ReceiveAction(MechaActionType.UNJAM, kv.Key);
                ((JammingRoom)m_otherMecha.GetRoom(RoomType.JAMMING)).StopJammingAction();
            }
        }
    }

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
            case MechaActionType.FIX:
                m_fixRoom.DoAction(GetRoom(targetRoomType),
                                     () => ReceiveAction(mechaActionType, targetRoomType));
                break;
            case MechaActionType.LOAD:
                ReceiveAction(mechaActionType, targetRoomType);
                break;
            case MechaActionType.POP_RESOURCE:
                if (targetRoomType == RoomType.AMMUNITION)
                {
                    m_munRoom.DoAction(null, null);
                } else if (targetRoomType == RoomType.ENERGY)
                {
                    m_eneRoom.DoAction(null, null);
                }
                break;
        }
    }

    public void ReceiveAction(MechaActionType actionType, RoomType roomType)
    {
        if (actionType == MechaActionType.JAMMING)
        {
            m_roomJammedTimes[roomType] = Time.time;
        }

        // Prevent from defending several rooms at the same time
        if (actionType == MechaActionType.DEFENCE)
        {
            ReceiveAction(MechaActionType.BREAK_DEFENCE, RoomType.ATTACK);
            ReceiveAction(MechaActionType.BREAK_DEFENCE, RoomType.DEFENCE);
            ReceiveAction(MechaActionType.BREAK_DEFENCE, RoomType.JAMMING);
        }

        switch (roomType)
        {
            case RoomType.ATTACK:
                m_attackRoom.OnReceive(actionType);
                break;
            case RoomType.DEFENCE:
                m_defenceRoom.OnReceive(actionType);

                if (m_defenceRoom.IsDamaged || m_defenceRoom.IsJammed)
                {
                    ReceiveAction(MechaActionType.BREAK_DEFENCE, RoomType.ATTACK);
                    ReceiveAction(MechaActionType.BREAK_DEFENCE, RoomType.DEFENCE);
                    ReceiveAction(MechaActionType.BREAK_DEFENCE, RoomType.JAMMING);
                }
                break;
            case RoomType.JAMMING:
                m_jammingRoom.OnReceive(actionType);

                // Unjam other mecha's rooms if our jamming room is out of service
                if (m_jammingRoom.IsDamaged || m_jammingRoom.IsJammed)
                {
                    m_otherMecha.ReceiveAction(MechaActionType.UNJAM, RoomType.ATTACK);
                    m_otherMecha.ReceiveAction(MechaActionType.UNJAM, RoomType.DEFENCE);
                    m_otherMecha.ReceiveAction(MechaActionType.UNJAM, RoomType.JAMMING);
                }
                break;
            case RoomType.FIX:
                m_fixRoom.OnReceive(actionType);
                break;
            case RoomType.AMMUNITION:
                m_munRoom.OnReceive(actionType);
                break;
            case RoomType.ENERGY:
                m_eneRoom.OnReceive(actionType);
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
            case RoomType.FIX:
                return m_fixRoom;
            case RoomType.AMMUNITION:
                return m_munRoom;
            case RoomType.ENERGY:
                return m_eneRoom;
        }
        return null;
    }

    public List<Room> GetFixableRooms()
    {
        List<Room> fixableRooms = new List<Room>(3);

        if (m_attackRoom.IsDamaged)
        {
            fixableRooms.Add(m_attackRoom);
        }
        if (m_defenceRoom.IsDamaged)
        {
            fixableRooms.Add(m_defenceRoom);
        }
        if (m_jammingRoom.IsDamaged)
        {
            fixableRooms.Add(m_jammingRoom);
        }

        return fixableRooms;
    }
}
