using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveListenner : ScriptableObject
{
    public UnitEvent m_Event;
    [HideInInspector] public Ability m_Owner { get; private set; }
    public void Awake()
    {
        //Debug.Log(this + " is Created.");
    }

    public virtual void Init(Ability ability)
    {
        m_Owner = ability;
        m_Event.RegisterListenner(m_Owner.m_Owner, Trigger);
    }

    public virtual void OnDestroy()
    {
        m_Event.UnregisterListenner(m_Owner.m_Owner, Trigger);
    }

    public virtual void Trigger(GameEventData eventData)
    {
        Debug.Log(this + " is Triggered.");
    }

    //protected abstract void Register();
    //protected abstract void Unregister();
}
