using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Condition/Formation")]
public class FormationCheck : ConditionCheck
{
    public override bool Check(AbilityInfo[] infos)
    {
        base.Check(infos);

        //Get Info
        SourceInfo sourceInfo = GetAbilityInfo<SourceInfo>();
        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();

        return IsFormation(sourceInfo.m_Source, targetInfo.m_Targets);
    }

    private bool IsFormation(Unit source, List<Unit> targets)
    {
        bool property = source.m_Data.GetStat(UnitStatsProperty.Formation) != 0;
        if(property)
            return true;

        //check number
        if (targets.Count < 2)
            return false;

        //check type and power
        int power = source.m_Data.GetStat(UnitStatsProperty.POW);
        foreach (Unit target in targets)
        {
            //check type
            if (!target.m_Data.IsSpiritType(SpiritType.Legion))
                return false;

            //check power
            int p = target.m_Data.GetStat(UnitStatsProperty.POW);

            if (p < power)
                return false;
        }

        return true;
    }
}
