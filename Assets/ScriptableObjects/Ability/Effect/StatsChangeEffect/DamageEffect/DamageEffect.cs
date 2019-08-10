using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Effect/DamageEffect")]
public class DamageEffect : StatsChangeEffect
{
    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        //Get Info
        SourceInfo sourceInfo = GetAbilityInfo<SourceInfo>();
        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();

        CreateEventData(sourceInfo, targetInfo);

        TrigggerBeforeActionEvent(sourceInfo.m_Source);

        //get source power;
        int pow = sourceInfo.GetPower();

        m_SourceDelta += sourceInfo.m_Source.m_Data.GetStat(UnitStatsProperty.Breaker);

        foreach (Unit unit in targetInfo.m_Targets)
        {
            m_TargetDelta = 0;

            TriggerBeforeReceiveEffectEvent(unit);

            int value = GetValue(unit, pow);
            if (value > 0)
                Damage(unit, value);

            TriggerAfterReceiveEffectEvent(unit);
        }
    }

    private void Damage(Unit target, int amount)
    {
        //Debug.Log("Damage " + target + " " + amount);
        target.m_Data.ChangeStats(UnitStatsProperty.DUR, -amount);
    }
}
