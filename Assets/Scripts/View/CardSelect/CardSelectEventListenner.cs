using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardSelectDisplay))]
public class CardSelectEventListenner : MonoBehaviour
{
    public GameEvent m_CardSelectEvent;
    public CardSelectDisplay m_Display;

    private void Awake()
    {
        Register();
        m_Display = GetComponent<CardSelectDisplay>();
    }

    private void OnDestroy()
    {
        Unregister();
    }

    public void Register()
    {
        m_CardSelectEvent.RegisterListenner(Show);
    }

    public void Unregister()
    {
        m_CardSelectEvent.UnregisterListenner(Show);
    }

    public void Show(GameEventData eventData)
    {
        //Debug.Log("Card Selection Triggered");
        CardSelectOptionsData data = eventData.CastDataType<CardSelectOptionsData>();
        if (data == null)
            return;

        if(data.m_Switch)
        {
            if (!data.isCondition)
                m_Display.DisplayOptions(data.unitDatas);
            else
                m_Display.DisplayOptions(data.conditions);
        }
        else
        {
            m_Display.Hide();
        }
    }
}
