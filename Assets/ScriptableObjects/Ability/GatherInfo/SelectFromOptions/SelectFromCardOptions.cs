﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/SelectFromCardOptions")]
public class SelectFromCardOptions : AbilityInfoGetter
{
    public delegate void CancelCallback();
    public CancelCallback cancelCallback;

    public GameEvent m_CardSelectEvent;
    public GameEvent m_CancalSelectEvent;

    [Header("Select")]
    public int m_Num;
    [Tooltip("Can confirm with number left isn't 0")]
    public bool m_CanEndEarly = false;

    [Tooltip("Can cancel selection")]
    public bool m_CanCancel = true;

    [Tooltip("Can select same target multiple times")]
    public bool m_CanRepeat;

    [Header("Highlight")]
    public HighlightType m_selectedCardType;

    [Header("Words to Display")]
    public string m_Message;

    protected int numberLeft;
    [HideInInspector]public List<GameObject> m_selectedCardPreviewList;

    public void GetInfo(CancelCallback method)
    {
        cancelCallback = method;

        Unit none = null;
        GetInfo(none);
    }

    public override void GetInfo(Unit source)
    {
        m_Done = false;

        m_selectedCardPreviewList = new List<GameObject>();

        numberLeft = m_Num;

        DisplayInfo();

        Register();

    }

    public override void StoreInfo(AbilityInfo info)
    {
        throw new System.NotImplementedException();
    }

    private void DisplayInfo()
    {
        ActionManager.Instance.m_ActionMessage.Display(m_Message + "(" + numberLeft + "/" + m_Num +")");

        ActionManager.Instance.m_ConfirmButton.Show();
        ActionManager.Instance.m_ConfirmButton.callback += Done;
    }

    public void SelectTrigger(GameEventData eventData)
    {
        CardPreviewData data = eventData.CastDataType<CardPreviewData>();
        if (data == null)
            return;

        GameObject cardPreview = data.m_Card;
        if (!m_CanRepeat && m_selectedCardPreviewList.Contains(cardPreview))
        {
            UnselectTarget(cardPreview);
            return;
        }

        if (numberLeft <= 0)
            return;

        m_selectedCardPreviewList.Add(cardPreview);

        HighlightTarget(cardPreview);

        numberLeft--;

        DisplayInfo();
    }

    private void Register()
    {
        m_CardSelectEvent.RegisterListenner(SelectTrigger);
        m_CancalSelectEvent.RegisterListenner(Cancel);
    }

    private void Unregister()
    {
        m_CardSelectEvent.UnregisterListenner(SelectTrigger);
        m_CancalSelectEvent.UnregisterListenner(Cancel);
    }

    public void Cancel(GameEventData data = null)
    {
        //Debug.Log("Cancel Select : " + m_Blocks.Count);
        if (m_selectedCardPreviewList.Count == 0)
        {
            if (!m_CanCancel)
                return;

            HideInfo();
            Unregister();
            UnhighlightAll();

            if (cancelCallback != null)
                cancelCallback.Invoke();
            else
                ActionManager.Instance.CancelAction();

            cancelCallback = null;
        }
        else
            UnselectTarget();
    }

    private void UnselectTarget(GameObject cardPreview = null)
    {
        if (cardPreview == null)
        {
            //get last target
            GameObject lastCard = m_selectedCardPreviewList[m_selectedCardPreviewList.Count - 1];
            m_selectedCardPreviewList.RemoveAt(m_selectedCardPreviewList.Count - 1);

            if (!m_selectedCardPreviewList.Contains(lastCard))
                HighlightTarget(lastCard, false);
        }
        else
        {
            m_selectedCardPreviewList.Remove(cardPreview);
            if (!m_selectedCardPreviewList.Contains(cardPreview))
                HighlightTarget(cardPreview, false);
        }

        numberLeft++;
        DisplayInfo();
    }

    private void Done()
    {
        if (!m_CanEndEarly && numberLeft > 0)
            return;

        cancelCallback = null;

        HideInfo();

        UnhighlightAll();

        Unregister();

        m_Done = true;
    }

    private void HideInfo()
    {
        ActionManager.Instance.m_ActionMessage.Hide();

        ActionManager.Instance.m_ConfirmButton.Hide();
    }

    private void UnhighlightAll()
    {
        foreach(GameObject card in m_selectedCardPreviewList)
        {
            HighlightTarget(card, false);
        }
    }

    private void HighlightTarget(GameObject card, bool highlight = true)
    {
        if (highlight)
            HighlightManager.Instance.Highlight(card, m_selectedCardType);
        else
            HighlightManager.Instance.Unhighlight(card, m_selectedCardType);
    }
}