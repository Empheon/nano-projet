using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class InteractiveConsole : InteractiveItem
{
    [SerializeField]
    private RoomType m_roomType;
    [SerializeField]
    private MechaActionType m_actionType;

    public Action<RoomType> OnHover;
    public Action<RoomType> OnUnHover;
    public Action<RoomType> OnOpenRoomPanel;
    public Action<RoomType> OnCloseRoomPanel;
    public Action<RoomType, MechaActionType> OnDoAction;

    private SpriteRenderer m_renderer;

    private Room m_room;
    private bool m_isCharacterFocused;
    private bool m_hoverCalled;
    private Mecha m;
    private void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Mecha mecha)
    {
        m = mecha;
        m_room = mecha.GetRoom(m_roomType);
    }

    private void Update()
    {
        if (m_room == null)
        {
            return;
        }

        Debug.Log(m.name + " " + m_actionType + " " + m_roomType + " " + m_isCharacterFocused + " " + m_room.CanDoAction());

      
        if (!m_hoverCalled && m_isCharacterFocused && CheckIfCanInteract())
        {
            m_hoverCalled = true;
            OnHover?.Invoke(m_roomType);
            m_renderer.DOColor(Color.cyan, 0.2f);
        } else if (m_hoverCalled && (!m_isCharacterFocused || !CheckIfCanInteract()))
        {
            m_hoverCalled = false;
            OnUnHover?.Invoke(m_roomType);
            m_renderer.DOColor(Color.white, 0.2f);
        }
    }

    private bool CheckIfCanInteract()
    {
        if (m_actionType == MechaActionType.LOAD)
        {
            return !m_room.IsLoaded;
        } else if (false /* check if there are fixable rooms */)
        {
            
        } else
        {
            return m_room.CanDoAction();
        }
    }

    protected override void OnCharacterBlur()
    {
        m_isCharacterFocused = false;
    }

    protected override void OnCharacterFocus()
    {
        m_isCharacterFocused = true;
    }

    protected override void OnCharacterInteractPositive(GameObject character)
    {
        // If the action is unavailable action, do not do it
        if (!m_hoverCalled)
        {
            return;
        }

        switch(m_actionType)
        {
            // actions that require a room menu
            case MechaActionType.ATTACK:
            case MechaActionType.DEFENCE:
            case MechaActionType.JAMMING:
            case MechaActionType.FIX:
                OnOpenRoomPanel?.Invoke(m_roomType);
                break;
            case MechaActionType.LOAD:
            case MechaActionType.POP_RESOURCE:
                OnDoAction?.Invoke(m_roomType, m_actionType);
                break;
        }
    }

    protected override void OnCharacterInteractNegative(GameObject character)
    {
        switch (m_actionType)
        {
            // actions that require a room menu
            case MechaActionType.ATTACK:
            case MechaActionType.DEFENCE:
            case MechaActionType.JAMMING:
            case MechaActionType.FIX:
                OnCloseRoomPanel?.Invoke(m_roomType);
                break;
        }
    }

    protected override void OnEnterCharacterRange()
    {
    }

    protected override void OnExitCharacterRange()
    {
    }
}

