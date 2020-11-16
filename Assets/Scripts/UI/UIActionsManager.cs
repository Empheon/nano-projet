using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIActionsManager : MonoBehaviour
{
    // Consoles
    [SerializeField]
    private InteractiveConsole m_fixConsole;
    [SerializeField]
    private InteractiveConsole m_munConsole;
    [SerializeField]
    private InteractiveConsole m_eneConsole;

    [SerializeField]
    private InteractiveConsole m_loadAttConsole;
    [SerializeField]
    private InteractiveConsole m_loadDefConsole;

    [SerializeField]
    private InteractiveConsole m_actionAttConsole;
    [SerializeField]
    private InteractiveConsole m_actionDefConsole;
    [SerializeField]
    private InteractiveConsole m_actionJamConsole;

    // Panels
    [SerializeField]
    private GameObject m_fixPanel;
    [SerializeField]
    private GameObject m_attPanel;
    [SerializeField]
    private GameObject m_defPanel;
    [SerializeField]
    private GameObject m_jamPanel;


    [SerializeField]
    private Mecha m_mecha;

    private void Start()
    {
        foreach (InteractiveConsole ic in m_mecha.GetComponentsInChildren<InteractiveConsole>())
        {
            ic.Init(m_mecha);
            ic.OnOpenRoomPanel += OpenRoomPanel;
            ic.OnCloseRoomPanel += CloseRoomPanel;
            ic.OnDoAction += DoAction;
        }

        SubscribeToPanelButtons(m_attPanel, MechaActionType.ATTACK);
        SubscribeToPanelButtons(m_defPanel, MechaActionType.DEFENCE);
        SubscribeToPanelButtons(m_jamPanel, MechaActionType.JAMMING);
        SubscribeToPanelButtons(m_fixPanel, MechaActionType.FIX);

        // Force close all panels
        ClosePanelAnim(m_attPanel, 0);
        ClosePanelAnim(m_defPanel, 0);
        ClosePanelAnim(m_jamPanel, 0);
        ClosePanelAnim(m_fixPanel, 0);
    }

    private void SubscribeToPanelButtons(GameObject panel, MechaActionType actionType)
    {
        foreach (UIActionRoomItem roomItem in panel.GetComponentsInChildren<UIActionRoomItem>())
        {
            roomItem.Init(actionType);
            roomItem.OnDoAction += DoAction;
        }
    }

    private void OpenRoomPanel(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.ATTACK:
                OpenPanelAnim(m_attPanel);
                break;
            case RoomType.DEFENCE:
                OpenPanelAnim(m_defPanel);
                break;
            case RoomType.JAMMING:
                OpenPanelAnim(m_jamPanel);
                break;
            case RoomType.FIX:
                OpenPanelAnim(m_fixPanel);
                break;
        }
    }

    private void CloseRoomPanel(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.ATTACK:
                ClosePanelAnim(m_attPanel);
                break;
            case RoomType.DEFENCE:
                ClosePanelAnim(m_defPanel);
                break;
            case RoomType.JAMMING:
                ClosePanelAnim(m_jamPanel);
                break;
            case RoomType.FIX:
                ClosePanelAnim(m_fixPanel);
                break;
        }
    }

    private void OpenPanelAnim(GameObject gO, float duration = 0.2f)
    {
        gO.transform.DOScale(1, duration);
    }

    private void ClosePanelAnim(GameObject gO, float duration = 0.2f)
    {
        gO.transform.DOScale(0, duration);
    }

    private void DoAction(RoomType targetRoomType, MechaActionType mechaActionType)
    {
        m_mecha.DoAction(mechaActionType, targetRoomType);
    }

    //private void OnActionsVisibilityChange(bool b)
    //{
    //    // TODO change this to a show/hide system when character is near
    //    if (b)
    //    {
    //        DisplayButton.enabled = true;
    //        DisplayButton.transform.DOScale(1, 0.2f);
    //    } else
    //    {
    //        DisplayButton.enabled = false;
    //        DisplayButton.transform.DOScale(0, 0.2f);
    //        HidePanel();
    //    }
    //}

    //private void OnFixVisibilityChange(bool b)
    //{
    //    // TODO change this to a show/hide system when character is near
    //    if (b)
    //    {
    //        FixButton.enabled = true;
    //        FixButton.transform.DOScale(1, 0.2f);
    //    } else
    //    {
    //        FixButton.enabled = false;
    //        FixButton.transform.DOScale(0, 0.2f);
    //    }
    //}

    //private void OnLoadVisibilityChange(bool b)
    //{
    //    // Loading is optional
    //    if (LoadButton == null || !LoadButton.gameObject.activeSelf)
    //    {
    //        return;
    //    }

    //    // TODO change this to a show/hide system when character is near
    //    if (b)
    //    {
    //        LoadButton.enabled = true;
    //        LoadButton.transform.DOScale(1, 0.2f);
    //    } else
    //    {
    //        LoadButton.enabled = false;
    //        LoadButton.transform.DOScale(0, 0.2f);
    //    }
    //}

    //public void SwitchPanelVisibility()
    //{
    //    if (m_isPanelVisible)
    //    {
    //        HidePanel();
    //    } else
    //    {
    //        DisplayPanel();
    //    }
    //}

    //public void DisplayPanel()
    //{
    //    m_isPanelVisible = true;
    //    m_actionPanel.transform.DOScale(1, 0.2f);
    //}

    //public void HidePanel()
    //{
    //    m_isPanelVisible = false;
    //    m_actionPanel.transform.DOScale(0, 0.2f);
    //}

    //public void DoFixAction()
    //{
    //    m_mecha.DoAction(MechaActionType.FIX, m_roomType);
    //}

    //public void DoLoadAction()
    //{
    //    m_mecha.DoAction(MechaActionType.LOAD, m_roomType);
    //}



}
