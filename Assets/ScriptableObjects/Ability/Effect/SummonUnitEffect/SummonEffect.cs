using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Effect/Summon")]
public class SummonEffect : Effect
{
    public UnitData m_Unit;

    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        TargetInfo info1 = GetAbilityInfo<TargetInfo>();
        if (info1 == null)
            return;

        foreach(FieldBlock block in info1.m_Blocks)
        {
            if(block.m_Unit == null)
            {
                SummonManager.Summon(m_Unit, block, block.m_PlayerID);
            }
        }
    }
}
