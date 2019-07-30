using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay))]
public class CardPreviewClick : EmptyClick
{
    private CardDisplay display;
    private CardDisplay cardDisplay
    {
        get
        {
            if (display == null)
                display = GetComponent<CardDisplay>();

            return display;
        }
    }
    protected override void TriggerLeftClickEvent()
    {
        if (m_LeftClickEvent == null)
            return;

        CardPreviewData data = new CardPreviewData();
        data.m_UnitData = cardDisplay.m_CardData;
        data.m_Card = gameObject;

        m_LeftClickEvent.Trigger(data);
    }

    protected override void TriggerRightClickEvent()
    {
        if (m_RightClickEvent == null)
            return;

        //Debug.Log("Display ["+ cardDisplay.m_CardData + "]");
        CardInfoData data = new CardInfoData();
        data.m_Data = cardDisplay.m_CardData;
        data.m_Switch = true;

        m_RightClickEvent.Trigger(data);

        //Debug.Log("Triggered [" + m_RightClickEvent + "]");
    }
}
