using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Status/StatsChange")]
public class StatsChangeStatus : Status
{
    public UnitStatsProperty m_Property;
    public int m_ChangedValue;


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
        m_EndEffectEvent.RegisterListenner(End);
    }

    protected override void Unregister()
    {
        m_EndEffectEvent.UnregisterListenner(End);
    }

    private void End(GameEventData data = null)
    {
        m_Duration--;
        if (m_Duration <= 0)
            m_Status.RemoveStatus(this);
    }
}
