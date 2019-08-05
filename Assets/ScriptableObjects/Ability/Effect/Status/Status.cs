using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : ScriptableObject
{
    public UnitStatus m_Status;

    [Header("Info")]
    public TextProperty m_StatusName;
    public TextProperty m_Description;

    [Header("Duration")]
    public int m_Duration;
    public GameEvent m_EndEffectEvent;

    public virtual string GetDescription()
    {
        return m_Description.ToString();
    }

    public virtual void AffectUnit(UnitStatus unitStatus)
    {
        m_Status = unitStatus;
        Regiseter();
    }

    public virtual void EndAffect()
    {
        Unregister();
    }

    protected virtual void Regiseter()
    {

    }

    protected virtual void Unregister()
    {

    }

    protected virtual void RemoveThis()
    {

    }
}
