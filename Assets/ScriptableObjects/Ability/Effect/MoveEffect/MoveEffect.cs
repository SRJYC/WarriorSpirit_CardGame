using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effect/Move")]
public class MoveEffect : Effect
{
    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        //Get Info
        SourceInfo sourceInfo = GetAbilityInfo<SourceInfo>();
        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();

        FieldBlock block = targetInfo.m_Blocks[0];
        if (block.m_Unit == null)
        {
            sourceInfo.m_Source.m_Position.MoveTo(block);
        }
    }
}
