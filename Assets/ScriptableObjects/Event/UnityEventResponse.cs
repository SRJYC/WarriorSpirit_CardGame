using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEventResponse : MonoBehaviour
{
    public GameEvent m_Event;
    public UnityEngine.Events.UnityEvent m_Response;

    private void OnEnable()
    {
        m_Event.RegisterListenner(Trigger);
    }

    private void OnDisable()
    {
        m_Event.UnregisterListenner(Trigger);
    }

    public void Trigger(GameEventData eventData = null)
    {
        m_Response.Invoke();
    }
}
