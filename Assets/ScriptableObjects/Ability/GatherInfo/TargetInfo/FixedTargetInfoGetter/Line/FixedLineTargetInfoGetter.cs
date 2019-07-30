using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/FixedLineGetter")]
public class FixedLineTargetInfoGetter : FixedTargetInfoGetter
{
    [Header("The Line (choose Center for targetting Warrior)")]
    public bool m_SameRowAsSourceUnit;
    public bool m_SameColumnAsSourceUnit;
    public FieldBlockType m_LineType;

    protected override void ConcreteGather()
    {
        if (m_SameColumnAsSourceUnit && !m_SameRowAsSourceUnit)
            m_LineType = m_Position.m_ColumnType;
        else if (!m_SameColumnAsSourceUnit && m_SameRowAsSourceUnit)
            m_LineType = m_Position.m_RowType;

        m_Blocks.AddRange(m_Field.GetBlocksOfLine(m_LineType));

    }
}
