using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPropertyMechanic : ScriptableObject
{
    public UnitEvent m_Event;

    public void Register(Unit unit)
    {
        if(m_Event != null)
        {
            m_Event.RegisterListenner(unit, Trigger);
        }
    }

    public void Unregister(Unit unit)
    {
        if (m_Event != null)
        {
            m_Event.UnregisterListenner(unit, Trigger);
        }
    }

    protected virtual void Trigger(GameEventData eventData)
    {

    }
}
