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
            case MechaActionType.LOAD:
                ReceiveAction(mechaActionType, targetRoomType);
                break;
        }
    }

    public void ReceiveAction(MechaActionType actionType, RoomType roomType)
    {
        if (actionType == MechaActionType.JAMMING)
        {
            m_roomJammedTimes[roomType] = Time.time;
        }

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

                // Unjam other mecha's rooms if our jamming room is out of service
                if (m_jammingRoom.IsDamaged || m_jammingRoom.IsJammed)
                {
                    m_otherMecha.ReceiveAction(MechaActionType.UNJAM, RoomType.ATTACK);
                    m_otherMecha.ReceiveAction(MechaActionType.UNJAM, RoomType.DEFENCE);
                    m_otherMecha.ReceiveAction(MechaActionType.UNJAM, RoomType.JAMMING);
                }
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
