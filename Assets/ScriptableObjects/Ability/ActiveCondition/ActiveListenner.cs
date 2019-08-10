using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Activate")]
public class ActiveListenner : ScriptableObject
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
        m_Owner.Trigger();
    }

    //protected abstract void Register();
    //protected abstract void Unregister();
}
