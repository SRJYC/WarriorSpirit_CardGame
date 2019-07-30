using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/FixedSingleGetter")]
public class FixedSingleTargetInfoGetter : FixedTargetInfoGetter
{
    [Header("Enemy")]
    [Tooltip("Get the block of enemy field within same column")] public FieldBlockType m_Row;
    [Tooltip("Move row, if that row has no unit")] public bool m_EnemyExtend;

    [Header("Ally")]
    public bool m_Self;
    public bool m_Couple;
    public bool m_AllyCoupleColumn;//Get the other ally block on the same column, exculde center

    [Header("Include adjcent units(Same Row)")]
    public bool m_Adjcent;

    protected override void ConcreteGather()
    {
        if (m_IsAlly)
            GetAllyTarget();
        else
            GetEnemyTarget();
    }

    private void GetEnemyTarget()
    {
        //get column
        FieldBlockType column = m_Position.m_ColumnType;

        //get primary target
        FieldBlock block = m_Field.GetBlockByPosition(m_Row, column);

        //check if extend
        if (m_EnemyExtend && block.m_Unit == null)
        {
            if (column != FieldBlockType.Middle)
                block = m_Field.GetCouple(block);
            else
                block = m_Field.GetBlocksOfLine(FieldBlockType.Center)[0];
        }

        //add block
        m_Blocks.Add(block);

        //check if include adjcent blocks
        if (m_Adjcent)
            m_Blocks.AddRange(m_Field.GetBlockBeside(block));
    }

    private void GetAllyTarget()
    {
        if (m_Self)
            m_Blocks.Add(m_Position);

        if (m_Couple)
            m_Blocks.Add(m_Field.GetCouple(m_Position, m_AllyCoupleColumn));

        if(m_Adjcent)
            m_Blocks.AddRange(m_Field.GetBlockBeside(m_Position));
    }
}
