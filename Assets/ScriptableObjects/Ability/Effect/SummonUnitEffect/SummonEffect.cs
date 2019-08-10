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

        FieldBlock block = GetBlock();

        if (block == null || block.m_Unit != null)
        {
            //Debug.LogError("Wrong Target");
            return;
        }

        SummonManager.Summon(m_Unit, block,block.m_PlayerID);
    }

    private FieldBlock GetBlock()
    {
        TargetInfo info = GetAbilityInfo<TargetInfo>();
        if (info == null || info.m_Blocks.Count <= 0)
            return null;

        return info.m_Blocks[0];
    }
}
