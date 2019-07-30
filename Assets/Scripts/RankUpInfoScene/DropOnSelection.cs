using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnSelection : MonoBehaviour
    ,IDropHandler
{
    public GameEvent changeSelectionEvent;
    public void OnDrop(PointerEventData eventData)
    {
        UnitData unit = eventData.pointerDrag.GetComponent<CardDisplay>().m_CardData;

        CardPreviewData data = new CardPreviewData();
        data.m_UnitData = unit;

        changeSelectionEvent.Trigger(data);
    }
}
