using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Effect/ApplyAura")]
public class ApplyAura : Effect
{
    public Aura m_Aura;

    [Header("End Aura")]
    public EndAuraStatus m_status;
    public UnitEvent m_EndEvent;

    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        SourceInfo sourceInfo = GetAbilityInfo<SourceInfo>();
        if (sourceInfo == null)
            return;
        Unit unit = sourceInfo.m_Source;

        Aura aura = AddAura(unit);

        ApplyStatus(unit, aura);
    }

    private Aura AddAura(Unit unit)
    {
        Aura aura = Instantiate(m_Aura);
        aura.Init(unit.m_PlayerID);
        AuraManager.Instance.AddAura(aura);

        return aura;
    }

    private void ApplyStatus(Unit unit, Aura aura)
    {
        EndAuraStatus status = Instantiate(m_status);
        status.m_Aura = aura;
        status.m_EndAuraEvent = m_EndEvent;

        unit.m_Status.ApplyStatus(status);
    }
}
