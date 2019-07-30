using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(FieldBlock))]
public class BlockClick : EmptyClick
{
    protected override void TriggerLeftClickEvent()
    {
        if (m_LeftClickEvent == null)
            return;

        FieldBlock block = GetComponent<FieldBlock>();
        SingleBlockData data = new SingleBlockData();
        data.m_Block = block;

        m_LeftClickEvent.Trigger(data);
    }

    protected override void TriggerRightClickEvent()
    {
    }
}
