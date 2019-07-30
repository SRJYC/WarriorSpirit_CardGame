using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/AllArea")]
public class AllTargetInfoGetter : FixedTargetInfoGetter
{
    protected override void ConcreteGather()
    {
        m_Blocks.AddRange(m_Field.m_blocks);
    }
}
