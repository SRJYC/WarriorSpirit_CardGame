using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwitch : MonoBehaviour
{
    public GameEvent m_Event;
    public UnitDataDisplay m_Display;

    private bool m_active = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Event.RegisterListenner(Trigger);
        gameObject.SetActive(m_active);
    }

    void OnDestroy()
    {
        m_Event.UnregisterListenner(Trigger);
    }

    public void Trigger(GameEventData eventData)
    {
        //Debug.Log("Receive Trigger");
        CardInfoData data = eventData.CastDataType<CardInfoData>();
        if (data == null)
            return;

        SetActive(data.m_Switch);

        if (m_active)
            m_Display.Display(data.m_Data);

        //Debug.Log("Active" + m_active);
        //Debug.Log("Data" + data.m_Data);
    }

    public void SetActive(bool active)
    {
        if (m_active == active)
            return;

        m_active = active;
        gameObject.SetActive(m_active);
    }
}
