using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Activate")]
public class ActiveListenner : ScriptableObject
{
    public UnitEvent m_Event;

    [Header("Ignore event when in one of these states")]
    public List<StateID> m_NotActivateStates;
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
        if (!CheckState())
            return;

        Debug.Log(this + " is Triggered.");
        m_Owner.Trigger();
    }

    private bool CheckState()
    {
        foreach(StateID id in m_NotActivateStates)
        {
            if (StateMachineManager.Instance.IsState(id))
                return false;
        }
        return true;
    }
}
