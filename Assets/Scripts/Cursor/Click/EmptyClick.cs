using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptyClick : MonoBehaviour
    , IPointerClickHandler
{
    public GameEvent m_RightClickEvent;
    public GameEvent m_LeftClickEvent;

    public void OnPointerClick(PointerEventData data)
    {
        //right click card
        if (data.button == PointerEventData.InputButton.Right)
        {
            TriggerRightClickEvent();
        }
        else if (data.button == PointerEventData.InputButton.Left)
        {
            TriggerLeftClickEvent();
        }
    }

    protected virtual void TriggerLeftClickEvent()
    {
        if (m_LeftClickEvent != null)
            m_LeftClickEvent.Trigger();
    }

    protected virtual void TriggerRightClickEvent()
    {
        //Debug.Log("Empty right click");
        if (m_RightClickEvent != null)
            m_RightClickEvent.Trigger();
    }
}