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
    [SerializeField]
    private Mecha m_mecha;
    [SerializeField]
    private RoomType m_roomType;
    [SerializeField]
    private MechaActionType m_actionType;

    // Temporary, to replace with hide/show when player is near
    public Button DisplayButton;
    public Button FixButton;
    public Button LoadButton;

    [SerializeField]
    private GameObject m_actionPanel;
    private bool m_isPanelVisible;

    private void Start()
    {
        m_mecha.GetRoom(m_roomType).OnActionableStatusChanged += OnActionsVisibilityChange;
        m_mecha.GetRoom(m_roomType).OnFixableStatusChanged += OnFixVisibilityChange;
        m_mecha.GetRoom(m_roomType).OnLoadableStatusChanged += OnLoadVisibilityChange;

        foreach (UIActionRoomItem item in GetComponentsInChildren<UIActionRoomItem>())
        {
            item.Init(m_mecha, m_actionType);
        }

        // force hide
        m_isPanelVisible = false;
        m_actionPanel.transform.DOScale(0, 0);
        FixButton.transform.DOScale(0, 0);

        // force show
        if (LoadButton != null && LoadButton.gameObject.activeSelf)
        {
            LoadButton.enabled = true;
            LoadButton.transform.DOScale(1, 0);
        }
    }

    private void OnActionsVisibilityChange(bool b)
    {
        // TODO change this to a show/hide system when character is near
        if (b)
        {
            DisplayButton.enabled = true;
            DisplayButton.transform.DOScale(1, 0.2f);
        } else
        {
            DisplayButton.enabled = false;
            DisplayButton.transform.DOScale(0, 0.2f);
            HidePanel();
        }
    }

    private void OnFixVisibilityChange(bool b)
    {
        // TODO change this to a show/hide system when character is near
        if (b)
        {
            FixButton.enabled = true;
            FixButton.transform.DOScale(1, 0.2f);
        } else
        {
            FixButton.enabled = false;
            FixButton.transform.DOScale(0, 0.2f);
        }
    }

    private void OnLoadVisibilityChange(bool b)
    {
        // Loading is optional
        if (LoadButton == null || !LoadButton.gameObject.activeSelf)
        {
            return;
        }

        // TODO change this to a show/hide system when character is near
        if (b)
        {
            LoadButton.enabled = true;
            LoadButton.transform.DOScale(1, 0.2f);
        } else
        {
            LoadButton.enabled = false;
            LoadButton.transform.DOScale(0, 0.2f);
        }
    }

    public void SwitchPanelVisibility()
    {
        if (m_isPanelVisible)
        {
            HidePanel();
        } else
        {
            DisplayPanel();
        }
    }

    public void DisplayPanel()
    {
        m_isPanelVisible = true;
        m_actionPanel.transform.DOScale(1, 0.2f);
    }

    public void HidePanel()
    {
        m_isPanelVisible = false;
        m_actionPanel.transform.DOScale(0, 0.2f);
    }

    public void DoFixAction()
    {
        m_mecha.DoAction(MechaActionType.FIX, m_roomType);
    }

    public void DoLoadAction()
    {
        m_mecha.DoAction(MechaActionType.LOAD, m_roomType);
    }
}
