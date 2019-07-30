using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Unit))]
public class HoverOnCard : HoverOnTrigger
{
    void Start()
    {
    }

    protected override void TriggerEnterEvent()
    {
        //.Log("Trigger On");
        CardInfoData data = new CardInfoData();
        data.m_Data = GetComponent<Unit>().m_Data;
        data.m_Switch = true;

        m_Event.Trigger(data);
    }

    protected override void TriggerExitEvent()
    {
        //Debug.Log("Trigger off");
        CardInfoData data = new CardInfoData();
        data.m_Switch = false;

        m_Event.Trigger(data);
    }
}
