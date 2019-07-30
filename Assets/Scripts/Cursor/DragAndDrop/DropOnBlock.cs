using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnBlock : MonoBehaviour
    , IDropHandler
{
    public GameEvent m_SummonEvent;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop");
        Unit u = eventData.pointerDrag.GetComponent<Unit>();

        if (u == null)
            return;

        SummonData data = new SummonData();
        data.m_Unit = u;
        data.m_Block = GetComponent<FieldBlock>();

        m_SummonEvent.Trigger(data);
    }
}
