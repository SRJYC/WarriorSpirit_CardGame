using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Status/StatsChange")]
public class StatsChangeStatus : Status
{
    public UnitStatsProperty m_Property;
    public int m_ChangedValue;

    public override string GetDescription()
    {
        if (m_Description != null)
            return m_Description.ToString();

        string and = m_ChangedValue >= 0 ? " +" : " ";
        return AllTextEnumProxy.Instance.GetText(m_Property) + and + m_ChangedValue;
    }

    public override void AffectUnit(UnitStatus unitStatus)
    {
        base.AffectUnit(unitStatus);

        m_Status.ChangeStats(m_Property, m_ChangedValue, false, true);
    }

    public override void EndAffect()
    {
        base.EndAffect();

        m_Status.ChangeStats(m_Property, -m_ChangedValue, false, false);
    }

    protected override void Regiseter()
    {
        if (m_EndEffectEvent != null)
            m_EndEffectEvent.RegisterListenner(End);
    }

    protected override void Unregister()
    {
        if (m_EndEffectEvent != null)
            m_EndEffectEvent.UnregisterListenner(End);
    }

    private void End(GameEventData data = null)
    {
        if(m_Duration >= 0)
        {
            m_Duration--;
            if (m_Duration <= 0)
                m_Status.RemoveStatus(this);
        }
    }
}
