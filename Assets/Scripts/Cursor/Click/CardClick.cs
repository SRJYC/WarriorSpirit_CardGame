using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Unit))]
public class CardClick : EmptyClick
{
    private Unit m_Unit;
    private Unit unit
    {
        get
        {
            if (m_Unit == null)
                m_Unit = GetComponent<Unit>();

            return m_Unit;
        }
    }

    protected override void TriggerLeftClickEvent()
    {
        if (m_LeftClickEvent == null)
            return;

        FieldBlock block = unit.m_Position.m_Block;
        if (block != null)
        {
            SingleBlockData data = new SingleBlockData();
            data.m_Block = block;

            m_LeftClickEvent.Trigger(data);
        }
    }

    protected override void TriggerRightClickEvent()
    {
        if (m_RightClickEvent == null)
            return;

        //Debug.Log("Triggered"+m_Event);
        CardInfoData data = new CardInfoData();
        data.m_Data = unit.m_Data;
        data.m_Switch = true;
        m_RightClickEvent.Trigger(data);
    }
}
