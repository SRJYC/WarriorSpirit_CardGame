using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Condition/IsType")]
public class TargetTypeCheck : ConditionCheck
{
    [Header("Targets must be one of these types")]
    public List<SpiritType> spiritTypes;

    public override bool Check(AbilityInfo[] infos)
    {
        base.Check(infos);

        //Get Info
        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();

        foreach(Unit unit in targetInfo.m_Targets)
        {
            if (!CheckTargetType(unit))
                return false;
        }
        return true;
    }

    private bool CheckTargetType(Unit target)
    {
        foreach(SpiritType type in spiritTypes)
        {
            if (target.m_Data.IsSpiritType(type))
                return true;
        }
        return false;
    }
}
