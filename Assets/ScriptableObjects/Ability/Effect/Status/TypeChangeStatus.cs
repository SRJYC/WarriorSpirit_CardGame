using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Status/TypeChange")]
public class TypeChangeStatus : Status
{
    public SpiritType m_Type;

    private bool change;

    public override void AffectUnit(UnitStatus unitStatus)
    {
        base.AffectUnit(unitStatus);

        change = !m_Status.m_Owner.m_Data.IsSpiritType(m_Type);

        if (change)
            m_Status.m_SpiritTypes.Add(m_Type);
        else
            End();
    }

    public override void EndAffect()
    {
        base.EndAffect();

        if (change)
            m_Status.m_SpiritTypes.Remove(m_Type);
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
        if(m_Duration<=0)
            m_Status.RemoveStatus(this);
    }
}
