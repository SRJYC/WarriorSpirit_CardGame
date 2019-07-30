using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Unit))]
public class DropOnCard : MonoBehaviour
    , IDropHandler
{
    public GameEvent m_RankUpEvent;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop");

        Unit unit = GetComponent<Unit>();
        FieldBlock block = unit.m_Position.m_Block;
        if(block == null)
        {
            Debug.Log("Can't Rank up in Hand");
            return;
        }

        Unit u = eventData.pointerDrag.GetComponent<Unit>();

        if(u.m_PlayerID != unit.m_PlayerID)
        {
            Debug.Log("Not same side");
            return;
        }

        RankUpData data = new RankUpData();
        data.data1 = unit.m_Data;
        data.data2 = u.m_Data;
        data.block = block;

        m_RankUpEvent.Trigger(data);
    }
}
